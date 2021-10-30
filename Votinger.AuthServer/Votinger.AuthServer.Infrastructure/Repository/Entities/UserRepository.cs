using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Votinger.AuthServer.Core.Entities;
using Votinger.AuthServer.Infrastructure.Data;
using Votinger.AuthServer.Infrastructure.Repository.Entities.Interfaces;

namespace Votinger.AuthServer.Infrastructure.Repository.Entities
{
    public class UserRepository : IUserRepository
    {
        private AuthServerDatabaseContext _context;
        private DbSet<User> _table;
        public UserRepository(AuthServerDatabaseContext context)
        {
            _context = context;
            _table = context.Set<User>();
        }

        public async Task<User> GetByIdAsync(int? id)
        {
            return await _table.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetByLoginAsync(string login)
        {
            return await _table.FirstOrDefaultAsync(x => x.Login == login);
        }

        public async Task<User> GetWithToken(int? id)
        {
            return await _table.Include(x => x.RefreshToken).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertAsync(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}