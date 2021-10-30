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
    public class PollAnswerOptionRepository : IPollAnswerOptionRepository
    {
        private DbSet<PollAnswerOption> _table;
        public PollAnswerOptionRepository(PollServerDatabaseContext context)
        {
            _table = context.Set<PollAnswerOption>();
        }

        public async Task<PollAnswerOption> GetByIdAsync(int? id)
        {
            return await _table.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<PollAnswerOption>> GetByIdsAsync(IEnumerable<int> ids)
        {
            return await _table.Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        public void Update(PollAnswerOption option)
        {
            _table.Update(option);
        }

        public void Update(IEnumerable<PollAnswerOption> options)
        {
            _table.UpdateRange(options);
        }
    }
}
