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
    public class PollRepository : IPollRepository
    {
        private DbSet<Poll> _table;
        public PollRepository(PollServerDatabaseContext context)
        {
            _table = context.Set<Poll>();
        }

        public async Task<IEnumerable<Poll>> GetFew(int from, int to, bool includeAnswers = false)
        {
            IQueryable<Poll> query = _table;
            if (includeAnswers)
            {
                query = _table.Include(x => x.AnswerOptions);
            }
            var toCount = to - from;
            if (toCount < 0)
                toCount = 0;
            return await query.Skip(from).Take(toCount).ToListAsync();
        }

        public async Task<Poll> GetByIdAsync(int? id, bool includeAnswers = false, bool includeRepliedUsers = false)
        {
            IQueryable<Poll> query = _table;
            if (includeAnswers)
            {
                query = _table.Include(x => x.AnswerOptions);
            }
            if (includeRepliedUsers)
            {
                query = _table.Include(x => x.AnswerOptions)
                    .ThenInclude(x => x.RepliedUsers);
            }
            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Poll> GetByPollIdAndUserId(int? pollId, int? userId, bool includeAnswers = false, bool includeRepliedUsers = false)
        {
            IQueryable<Poll> query = _table;
            if (includeAnswers)
            {
                query = _table.Include(x => x.AnswerOptions);
            }
            if (includeRepliedUsers)
            {
                query = _table.Include(x => x.AnswerOptions)
                    .ThenInclude(x => x.RepliedUsers.Where(y => y.UserId == userId));
            }
            return await query.FirstOrDefaultAsync(x => x.Id == pollId && x.UserId == userId);
        }

        public async Task InsertAsync(Poll poll)
        {
            await _table.AddAsync(poll);
        }

        public void Remove(Poll poll)
        {
            _table.Remove(poll);
        }
    }
}
