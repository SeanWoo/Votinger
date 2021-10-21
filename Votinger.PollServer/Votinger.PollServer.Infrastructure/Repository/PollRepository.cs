using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votinger.PollServer.Core.Entities;
using Votinger.PollServer.Infrastructure.Repository.Interfaces;

namespace Votinger.PollServer.Infrastructure.Repository
{
    public class PollRepository : IPollRepository
    {
        public Task<Poll> GetByIdAsync(int pollId)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(Poll poll)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Poll poll)
        {
            throw new NotImplementedException();
        }
    }
}
