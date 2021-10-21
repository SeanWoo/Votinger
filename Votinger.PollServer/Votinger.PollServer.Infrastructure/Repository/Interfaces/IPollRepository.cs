using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votinger.PollServer.Core.Entities;

namespace Votinger.PollServer.Infrastructure.Repository.Interfaces
{
    public interface IPollRepository
    {
        Task<Poll> GetByIdAsync(int pollId);
        Task InsertAsync(Poll poll);
        Task RemoveAsync(Poll poll);
    }
}
