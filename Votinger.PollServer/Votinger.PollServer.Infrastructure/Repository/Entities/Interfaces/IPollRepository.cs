using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votinger.PollServer.Core.Entities;

namespace Votinger.PollServer.Infrastructure.Repository.Entities.Interfaces
{
    public interface IPollRepository
    {
        Task<IEnumerable<Poll>> GetFew(int from, int to, bool includeAnswers = false);
        Task<Poll> GetByIdAsync(int? id, bool includeAnswers = false, bool includeRepliedUsers = false);
        Task<Poll> GetByPollIdAndUserId(int? pollId, int? userId, bool includeAnswers = false, bool includeRepliedUsers = false);
        Task InsertAsync(Poll poll);
        void Remove(Poll poll);
    }
}
