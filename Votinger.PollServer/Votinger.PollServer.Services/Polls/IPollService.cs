using System.Collections.Generic;
using System.Threading.Tasks;
using Votinger.PollServer.Core.Entities;
using Votinger.PollServer.Services.Polls.Model;

namespace Votinger.PollServer.Services.Polls
{
    public interface IPollService
    {
        Task CreatePollAsync(CreatePollModel model);
        Task VoteInPollAsync(AnswerPollModel model);
        Task CancelVoteInPollAsync(int pollId);
        Task RemovePollAsync(Poll poll);
        Task<Poll> GetPollByIdAsync(int pollId);
        Task<List<PollAnswerOption>> GetAnswersAsync(int pollId);
        Task IsAnsweredInPollAsync(int pollId);
    }
}
