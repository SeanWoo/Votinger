using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votinger.PollServer.Core.Entities;
using Votinger.PollServer.Infrastructure.Repository.Interfaces;

namespace Votinger.PollServer.Tests.MockRepositories
{
    class MockPollRepository : IPollRepository
    {
        private List<Poll> _pools = new List<Poll>();
        public async Task<Poll> GetByIdAsync(int pollId)
        {
            return _pools.FirstOrDefault(x => x.Id == pollId);
        }

        public async Task InsertAsync(Poll poll)
        {
            _pools.Add(poll);
        }

        public async Task RemoveAsync(Poll poll)
        {
            _pools.Remove(poll);
        }
    }
}
