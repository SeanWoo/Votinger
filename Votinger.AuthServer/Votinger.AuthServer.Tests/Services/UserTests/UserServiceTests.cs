using Moq;
using System.Threading.Tasks;
using Votinger.AuthServer.Core.Entities;
using Votinger.AuthServer.Infrastructure.Data;
using Votinger.AuthServer.Infrastructure.Repository.Entities;
using Votinger.AuthServer.Infrastructure.Repository.Entities.Interfaces;
using Votinger.AuthServer.Services.Jwt;
using Votinger.AuthServer.Services.Jwt.Models;
using Votinger.AuthServer.Services.Users;
using Xunit;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Collections.Generic;
using Votinger.AuthServer.Services.Users.Models;
using Votinger.AuthServer.Infrastructure.Repository;

namespace Votinger.AuthServer.Tests.Services.UserTests
{
    public class UserService_RefreshTokensTests
    {
        //jwt.io ID: 1
        const string refreshToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYXNkIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiYWRtaW4iLCJuYmYiOjE2MzIzMzI0MzQsImlkIjoiMSIsImV4cCI6MTkzMjM1NDAzNCwiaXNzIjoiVGVzdElzc3VlciIsImF1ZCI6IlRlc3RBdWRpZW5jZSJ9.Ssq4AvXTtBXpQQ1UaNRWWWIhCst1LgUO0jA0EyWnAVI";

        private IUnitOfWork _unitOfWork;
        private IRefreshTokenRepository _refreshTokenRepository;
        private IJwtService _jwtService;
        private User _testUser;
        private ClaimsPrincipal _testClaim;
        public UserService_RefreshTokensTests()
        {
            _testUser = new User()
            {
                Id = 1,
                Login = "testLogin",
                RefreshToken = new RefreshToken()
                {
                    Id = 1,
                    Token = refreshToken
                }
            };

            var claimIdentity = new ClaimsIdentity(new List<Claim>()
            {
                new Claim("id", "1")
            });
            _testClaim = new ClaimsPrincipal(claimIdentity);

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(x => x.GetWithToken(It.IsAny<int>())).ReturnsAsync(_testUser);

            var mockRefreshTokenRepository = new Mock<IRefreshTokenRepository>();
            mockRefreshTokenRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(_testUser.RefreshToken);
            mockRefreshTokenRepository.Setup(x => x.UpdateAsync(It.IsAny<RefreshToken>())).Callback(() => _testUser.RefreshToken.Token = "newTestRefreshToken");

            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(x => x[It.Is<string>(k => k == "JwtOptions:Issuer")]).Returns("TestIssuer");
            mockConfig.Setup(x => x[It.Is<string>(k => k == "JwtOptions:Audience")]).Returns("TestAudience");
            mockConfig.Setup(x => x[It.Is<string>(k => k == "JwtOptions:Lifetime")]).Returns("100");
            mockConfig.Setup(x => x[It.Is<string>(k => k == "JwtOptions:Secret")]).Returns("mytest_secretkey");

            var mockJwtService = new Mock<IJwtService>();
            mockJwtService.Setup(x => x.GenerateTokens(It.IsAny<User>())).Returns(new TokensModel("newTestAccessToken", "newTestRefreshToken"));
            mockJwtService.Setup(x => x.Validate(It.IsAny<string>())).Returns(_testClaim);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.Users).Returns(mockUserRepository.Object);
            mockUnitOfWork.Setup(x => x.RefreshTokens).Returns(mockRefreshTokenRepository.Object);

            _unitOfWork = mockUnitOfWork.Object;
            _jwtService = mockJwtService.Object;
        }

        [Fact]
        public async Task RefreshTokenAsync_User_Null()
        {
            var userService = new UserService(_unitOfWork, _jwtService);

            var tokens = await userService.RefreshTokenAsync("notValidToken");

            Assert.Null(tokens);
            Assert.Equal(refreshToken, _testUser.RefreshToken.Token);
        }
        [Fact]
        public async Task RefreshTokenAsync_User_Tokens()
        {
            var userService = new UserService(_unitOfWork, _jwtService);

            var tokens = await userService.RefreshTokenAsync(refreshToken);

            Assert.NotNull(tokens);
            Assert.Equal("newTestAccessToken", tokens.AccessToken);
            Assert.Equal("newTestRefreshToken", tokens.RefreshToken);
            Assert.Equal("newTestRefreshToken", _testUser.RefreshToken.Token);
        }

    }
}
