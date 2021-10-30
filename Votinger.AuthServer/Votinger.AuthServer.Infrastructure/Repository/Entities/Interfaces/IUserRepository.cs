using System.Threading.Tasks;
using Votinger.AuthServer.Core.Entities;

namespace Votinger.AuthServer.Infrastructure.Repository.Entities.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int? id);
        Task<User> GetByLoginAsync(string login);
        Task<User> GetWithToken(int? id);
        Task InsertAsync(User user);
    }
}
