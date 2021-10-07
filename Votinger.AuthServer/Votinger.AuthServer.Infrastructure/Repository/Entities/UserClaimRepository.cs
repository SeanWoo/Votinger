using Microsoft.EntityFrameworkCore;
using Votinger.AuthServer.Core.Entities;
using Votinger.AuthServer.Infrastructure.Data;
using Votinger.AuthServer.Infrastructure.Repository.Entities.Interfaces;

namespace Votinger.AuthServer.Infrastructure.Repository.Entities
{
    public class UserClaimRepository : IUserClaimRepository
    {
        private AuthServerDatabaseContext _context;
        private DbSet<User> _table;
        public UserClaimRepository(AuthServerDatabaseContext context)
        {
            _context = context;
            _table = context.Set<User>();
        }
    }
}
