using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votinger.PollServer.Core.Entities;

namespace Votinger.PollServer.Infrastructure.Data
{
    public class PollServerDatabaseContext : DbContext
    {
        public DbSet<Poll> Polls { get; set; }
        public DbSet<PollAnswerOption> PollAnswerOptions { get; set; }
        public PollServerDatabaseContext(DbContextOptions<PollServerDatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
