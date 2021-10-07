using Microsoft.EntityFrameworkCore;
using Votinger.AuthServer.Core.Entities;

namespace Votinger.AuthServer.Infrastructure.Data
{
    public class AuthServerDatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public AuthServerDatabaseContext(DbContextOptions<AuthServerDatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
