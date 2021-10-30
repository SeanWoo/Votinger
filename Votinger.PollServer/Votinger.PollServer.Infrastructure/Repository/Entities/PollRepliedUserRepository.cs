using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votinger.PollServer.Core.Entities;
using Votinger.PollServer.Infrastructure.Data;
using Votinger.PollServer.Infrastructure.Repository.Entities.Interfaces;

namespace Votinger.PollServer.Infrastructure.Repository.Entities
{
    public class PollRepliedUserRepository : IPollRepliedUserRepository
    {
        private DbSet<PollRepliedUser> _table;
        public PollRepliedUserRepository(PollServerDatabaseContext context)
        {
            _table = context.Set<PollRepliedUser>();
        }
        public async Task InsertAsync(PollRepliedUser entity)
        {
            await _table.AddAsync(entity);
        }

        public async Task InsertAsync(IEnumerable<PollRepliedUser> entity)
        {
            await _table.AddRangeAsync(entity);
        }

        public void Remove(PollRepliedUser entity)
        {
            _table.Remove(entity);
        }

        public void Remove(IEnumerable<PollRepliedUser> entities)
        {
            _table.RemoveRange(entities);
        }
    }
}
