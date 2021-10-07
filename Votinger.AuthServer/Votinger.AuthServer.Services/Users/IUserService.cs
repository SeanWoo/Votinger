using System.Threading.Tasks;
using Votinger.AuthServer.Services.Jwt.Models;
using Votinger.AuthServer.Services.Users.Models;

namespace Votinger.AuthServer.Services.Users
{
    /// <summary>
    /// User service manages users
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// User registration
        /// </summary>
        /// <returns>TokensModel -> return access_token and refresh_token</returns>
        Task<SignResponse> SignUpAsync(SignUpModel model);
        /// <summary>
        /// Login account
        /// </summary>
        /// <returns>TokensModel -> return access_token and refresh_token</returns>
        Task<SignResponse> SignInAsync(SignInModel model);
        /// <summary>
        /// Refresh access and refresh token
        /// </summary>
        /// <returns></returns>
        Task<TokensModel> RefreshTokenAsync(string refreshToken);
    }
}
