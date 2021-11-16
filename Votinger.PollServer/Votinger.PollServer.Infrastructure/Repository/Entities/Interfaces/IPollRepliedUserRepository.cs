using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votinger.PollServer.Core.Entities;

namespace Votinger.PollServer.Infrastructure.Repository.Entities.Interfaces
{
    public interface IPollRepliedUserRepository
    {
        Task<List<PollRepliedUser>> GetAnswersByUserId(int? userId);
        Task InsertAsync(PollRepliedUser entity);
        Task InsertAsync(IEnumerable<PollRepliedUser> entity);
        void Remove(PollRepliedUser entity);
        void Remove(IEnumerable<PollRepliedUser> entities);
    }
}
