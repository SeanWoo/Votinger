using System.Collections.Generic;
using System.Threading.Tasks;
using Votinger.PollServer.Core.Entities;
using Votinger.PollServer.Services.Polls.Model;

namespace Votinger.PollServer.Services.Polls
{
    public interface IPollService
    {
        Task<IEnumerable<Poll>> GetFew(int from, int to, bool includeAnswers = false);
        Task<Poll> GetPollByIdAsync(int pollId, bool includeAnswers = false, bool includeRepliedUsers = false);
        Task<Poll> CreatePollAsync(CreatePollModel model);
        Task<bool> VoteInPollAsync(AnswerPollModel model);
        Task<bool> CancelVoteInPollAsync(int userId, int pollId);
        Task<bool> RemovePollAsync(Poll poll);
    }
}
