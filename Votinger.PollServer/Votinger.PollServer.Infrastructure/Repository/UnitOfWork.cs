using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votinger.PollServer.Infrastructure.Data;
using Votinger.PollServer.Infrastructure.Repository.Entities;
using Votinger.PollServer.Infrastructure.Repository.Entities.Interfaces;

namespace Votinger.PollServer.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private PollServerDatabaseContext _context;

        private IPollRepository _polls;
        private IPollAnswerOptionRepository _pollAnswerOptions;
        private IPollRepliedUserRepository _pollRepliedUsers;

        public IPollRepository Polls => _polls is not null ? _polls : new PollRepository(_context);
        public IPollAnswerOptionRepository PollAnswerOptions => _pollAnswerOptions is not null ? _pollAnswerOptions : new PollAnswerOptionRepository(_context);
        public IPollRepliedUserRepository PollRepliedUsers => _pollRepliedUsers is not null ? _pollRepliedUsers : new PollRepliedUserRepository(_context);
        public UnitOfWork(PollServerDatabaseContext context)
        {
            _context = context;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
