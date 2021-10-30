using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votinger.PollServer.Core.Entities;
using Votinger.PollServer.Infrastructure.Repository;
using Votinger.PollServer.Services.Polls.Model;

namespace Votinger.PollServer.Services.Polls
{
    public class PollService : IPollService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PollService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Poll>> GetFew(int from, int to, bool includeAnswers = false)
        {
            return await _unitOfWork.Polls.GetFew(from, to, includeAnswers);
        }

        public async Task<Poll> GetPollByIdAsync(int pollId, bool includeAnswers = false, bool includeRepliedUsers = false)
        {
            return await _unitOfWork.Polls.GetByIdAsync(pollId, includeAnswers, includeRepliedUsers);
        }

        public async Task<bool> CancelVoteInPollAsync(int pollId, int userId)
        {
            var poll = await _unitOfWork.Polls.GetByPollIdAndUserId(pollId, userId, includeRepliedUsers: true);

            if (poll is null) return false;

            foreach (var option in poll.AnswerOptions)
            {
                if (option.RepliedUsers.Any(x => x.UserId == userId))
                {
                    option.NumberOfReplies -= 1;
                    _unitOfWork.PollRepliedUsers.Remove(option.RepliedUsers);
                    _unitOfWork.PollAnswerOptions.Update(option);
                }
            }

            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<Poll> CreatePollAsync(CreatePollModel model)
        {
            if (model is null)
                throw new ArgumentNullException();

            var poll = new Poll();
            poll.UserId = model.UserId;
            poll.Title = model.Title;
            poll.AnswerOptions = model.AnswerOptions.Select(x => new PollAnswerOption()
            {
                Text = x,
                NumberOfReplies = 0,
                Poll = poll
            }).ToList();

            await _unitOfWork.Polls.InsertAsync(poll);

            await _unitOfWork.SaveChangesAsync();

            return poll;
        }

        public async Task<bool> RemovePollAsync(Poll poll)
        {
            var result = await _unitOfWork.Polls.GetByPollIdAndUserId(poll.Id, poll.UserId);

            _unitOfWork.Polls.Remove(result);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> VoteInPollAsync(AnswerPollModel model)
        {
            var answerOptionIds = model.AnswerOptionIds.Distinct();
            var poll = await _unitOfWork.Polls.GetByPollIdAndUserId(model.PollId, model.UserId, includeRepliedUsers: true);
            if (poll is null) return false;

            var pollAnswerOptions = poll.AnswerOptions.Where(x => answerOptionIds.Contains(x.Id));
            if (!pollAnswerOptions.Any()) return false;

            if (poll.AnswerOptions.Any(x => x.RepliedUsers.Any())) return false;

            foreach (var option in pollAnswerOptions)
            {
                option.NumberOfReplies += 1;
            }

            _unitOfWork.PollAnswerOptions.Update(pollAnswerOptions);

            var pollRepliedUsers = answerOptionIds.Select(x => new PollRepliedUser()
            {
                UserId = model.UserId,
                PollAnswerOptionId = x
            });

            await _unitOfWork.PollRepliedUsers.InsertAsync(pollRepliedUsers);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
