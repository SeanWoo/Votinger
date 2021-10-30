using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votinger.PollServer.Infrastructure.Repository.Entities.Interfaces;

namespace Votinger.PollServer.Infrastructure.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IPollRepository Polls { get; }
        IPollAnswerOptionRepository PollAnswerOptions { get; }
        IPollRepliedUserRepository PollRepliedUsers { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
