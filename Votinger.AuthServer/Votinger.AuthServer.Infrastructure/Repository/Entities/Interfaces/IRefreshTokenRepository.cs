using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votinger.AuthServer.Core.Entities;

namespace Votinger.AuthServer.Infrastructure.Repository.Entities.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetByIdAsync(int? id);
        Task UpdateAsync(RefreshToken entity);
        Task InsertAsync(RefreshToken entity);
    }
}
