using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        public static readonly ILoggerFactory MyLoggerFactory
    = LoggerFactory.Create(builder => { builder.AddConsole(); });
        public DbSet<Poll> Polls { get; set; }
        public DbSet<PollAnswerOption> PollAnswerOptions { get; set; }
        public DbSet<PollRepliedUser> PollRepliedUsers { get; set; }
        public PollServerDatabaseContext(DbContextOptions<PollServerDatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(MyLoggerFactory);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
