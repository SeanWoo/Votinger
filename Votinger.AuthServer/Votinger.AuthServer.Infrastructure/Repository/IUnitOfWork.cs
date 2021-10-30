using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votinger.AuthServer.Infrastructure.Repository.Entities.Interfaces;

namespace Votinger.AuthServer.Infrastructure.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IUserClaimRepository UserClaims { get; }
        IRefreshTokenRepository RefreshTokens { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
