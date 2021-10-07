using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Votinger.AuthServer.Core.Entities;
using Votinger.AuthServer.Infrastructure.Data;
using Votinger.AuthServer.Infrastructure.Repository.Entities.Interfaces;

namespace Votinger.AuthServer.Infrastructure.Repository.Entities
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private AuthServerDatabaseContext _context;
        private DbSet<RefreshToken> _table;
        public RefreshTokenRepository(AuthServerDatabaseContext context)
        {
            _context = context;
            _table = context.Set<RefreshToken>();
        }

        public async Task<RefreshToken> GetByIdAsync(int? id)
        {
            return await _table.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(RefreshToken entity)
        {
            _table.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task InsertAsync(RefreshToken entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }
}
