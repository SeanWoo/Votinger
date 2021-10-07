using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Votinger.AuthServer.Data.Database
{
    public class AuthServerDatabaseContext : DbContext
    {
        public AuthServerDatabaseContext(DbContextOptions<AuthServerDatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
