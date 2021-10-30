using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votinger.AuthServer.Infrastructure.Data;
using Votinger.AuthServer.Infrastructure.Repository.Entities;
using Votinger.AuthServer.Infrastructure.Repository.Entities.Interfaces;

namespace Votinger.AuthServer.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private AuthServerDatabaseContext _context;

        private IUserRepository _users;
        private IUserClaimRepository _userClaims;
        private IRefreshTokenRepository _refreshTokens;

        public IUserRepository Users => _users is not null ? _users : new UserRepository(_context);
        public IUserClaimRepository UserClaims => _userClaims is not null ? _userClaims : new UserClaimRepository(_context);
        public IRefreshTokenRepository RefreshTokens => _refreshTokens is not null ? _refreshTokens : new RefreshTokenRepository(_context);

        public UnitOfWork(AuthServerDatabaseContext context)
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
