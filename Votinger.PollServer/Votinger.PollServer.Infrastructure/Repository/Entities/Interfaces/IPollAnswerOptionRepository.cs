using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votinger.PollServer.Core.Entities;

namespace Votinger.PollServer.Infrastructure.Repository.Entities.Interfaces
{
    public interface IPollAnswerOptionRepository
    {
        Task<PollAnswerOption> GetByIdAsync(int? id);
        Task<IEnumerable<PollAnswerOption>> GetByIdsAsync(IEnumerable<int> ids);
        void Update(PollAnswerOption option);
        void Update(IEnumerable<PollAnswerOption> options);
    }
}
