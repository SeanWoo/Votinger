using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votinger.PollServer.Core.Entities;
using Votinger.PollServer.Infrastructure.Repository.Interfaces;
using Votinger.PollServer.Services.Polls.Model;

namespace Votinger.PollServer.Services.Polls
{
    public class PollService : IPollService
    {
        private readonly IPollRepository _pollRepository;
        public PollService(IPollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }
        public async Task CancelVoteInPollAsync(int pollId)
        {
            throw new NotImplementedException();
        }

        public async Task CreatePollAsync(CreatePollModel model)
        {
            if (model is null)
                throw new ArgumentNullException();

            var poll = new Poll();

            poll.Title = model.Title;
            poll.AnswerOptions = model.AnswerOptions.Select(x => new PollAnswerOption()
            {
                Text = x,
                NumberOfReplies = 0,
                Poll = poll
            }).ToList();

            _pollRepository.InsertAsync(poll);
        }

        public async Task<List<PollAnswerOption>> GetAnswersAsync(int pollId)
        {
            throw new NotImplementedException();
        }

        public async Task<Poll> GetPollByIdAsync(int pollId)
        {
            return await _pollRepository.GetByIdAsync(pollId);
        }

        public async Task IsAnsweredInPollAsync(int pollId)
        {
            throw new NotImplementedException();
        }

        public async Task RemovePollAsync(Poll poll)
        {
            _pollRepository.RemoveAsync(poll);
        }

        public async Task VoteInPollAsync(AnswerPollModel model)
        {
            throw new NotImplementedException();
        }
    }
}
