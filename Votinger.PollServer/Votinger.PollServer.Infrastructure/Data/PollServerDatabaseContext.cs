using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Votinger.PollServer.Infrastructure.Data
{
    public class PollServerDatabaseContext : DbContext
    {
        public PollServerDatabaseContext(DbContextOptions<PollServerDatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
