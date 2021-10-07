using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Votinger.AuthServer.Core.Entities;
using Votinger.AuthServer.Infrastructure.Repository.Entities;
using Votinger.AuthServer.Infrastructure.Repository.Entities.Interfaces;
using Votinger.AuthServer.Services.Jwt;
using Votinger.AuthServer.Services.Jwt.Models;
using Votinger.AuthServer.Services.Users.Models;

namespace Votinger.AuthServer.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IJwtService _jwtService;

        public UserService(
            IUserRepository userRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IJwtService jwtService)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtService = jwtService;
        }

        public async Task<TokensModel> RefreshTokenAsync(string refreshToken)
        {
            var claimPrincipal = _jwtService.Validate(refreshToken);

            if (claimPrincipal is null)
                return null;

            var claim = claimPrincipal.Claims.FirstOrDefault(x => x.Type == "id");

            if (claim is null)
                return null;

            var userId = Convert.ToInt32(claim.Value);
            var user = await _userRepository.GetByIdAsync(userId);

            if (user is null || user.RefreshToken is null)
                return null;

            var refreshTokenEntity = await _refreshTokenRepository.GetByIdAsync(user.RefreshToken.Id);

            if (refreshTokenEntity.Token != refreshToken)
                return null;

            var tokens = _jwtService.GenerateTokens(user);

            refreshTokenEntity.Token = tokens.RefreshToken;

            await _refreshTokenRepository.UpdateAsync(refreshTokenEntity);

            return tokens;
        }

        public async Task<SignResponse> SignInAsync(SignInModel model)
        {
            if (model is null)
                throw new ArgumentNullException();

            var user = await _userRepository.GetByLoginAsync(model.Login);

            if (user is null || user.Password != model.Password)
                return null;

            var tokens = _jwtService.GenerateTokens(user);

            if (user.RefreshTokenId is null)
            {
                var refreshToken = new RefreshToken()
                {
                    Token = tokens.RefreshToken
                };
                user.RefreshToken = refreshToken;

                await _refreshTokenRepository.InsertAsync(refreshToken);
            }
            else 
            {
                var refreshToken = await _refreshTokenRepository.GetByIdAsync(user.RefreshTokenId);
                refreshToken.Token = tokens.RefreshToken;
                await _refreshTokenRepository.UpdateAsync(refreshToken);
            }

            return new(user, tokens);
        }

        public async Task<SignResponse> SignUpAsync(SignUpModel model)
        {
            if (model is null)
                throw new ArgumentNullException();

            var userFromDb = await _userRepository.GetByLoginAsync(model.Login);

            if (userFromDb is not null)
                return null;

            var user = new User()
            {
                Login = model.Login,
                Password = model.Password
            };
            var tokens = _jwtService.GenerateTokens(user);

            var refreshToken = new RefreshToken()
            {
                Token = tokens.RefreshToken
            };
            user.RefreshToken = refreshToken;

            await _userRepository.InsertAsync(user);

            return new (user, tokens);
        }
    }
}
