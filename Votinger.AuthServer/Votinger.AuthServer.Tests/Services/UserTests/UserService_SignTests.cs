using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Votinger.AuthServer.Core.Entities;
using Votinger.AuthServer.Infrastructure.Data;
using Votinger.AuthServer.Infrastructure.Repository;
using Votinger.AuthServer.Infrastructure.Repository.Entities;
using Votinger.AuthServer.Infrastructure.Repository.Entities.Interfaces;
using Votinger.AuthServer.Services.Jwt;
using Votinger.AuthServer.Services.Jwt.Models;
using Votinger.AuthServer.Services.Users;
using Votinger.AuthServer.Services.Users.Models;
using Xunit;

namespace Votinger.AuthServer.Tests.Services.UserTests
{
    public class UserService_SignTests
    {
        //jwt.io ID: 1
        const string refreshToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYXNkIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiYWRtaW4iLCJuYmYiOjE2MzIzMzI0MzQsImlkIjoiMSIsImV4cCI6MTkzMjM1NDAzNCwiaXNzIjoiVGVzdElzc3VlciIsImF1ZCI6IlRlc3RBdWRpZW5jZSJ9.Ssq4AvXTtBXpQQ1UaNRWWWIhCst1LgUO0jA0EyWnAVI";

        private IUnitOfWork _unitOfWork;
        private IJwtService _jwtService;

        public UserService_SignTests()
        {
            var options = new DbContextOptionsBuilder<AuthServerDatabaseContext>()
                .UseInMemoryDatabase("MockDBSign")
                .Options;

            var context = new AuthServerDatabaseContext(options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Users.Add(new() { Login = "test", Password = "testPass" });
            context.Users.Add(new() { Login = "test2", Password = "testPass2" });
            context.Users.Add(new() { Login = "test3", Password = "testPass3", RefreshToken = new RefreshToken() });

            context.SaveChanges();


            var mockJwtService = new Mock<IJwtService>();
            mockJwtService.Setup(x => x.GenerateTokens(It.IsAny<User>())).Returns(new TokensModel("newTestAccessToken", "newTestRefreshToken"));

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.Users).Returns(new UserRepository(context));
            mockUnitOfWork.Setup(x => x.RefreshTokens).Returns(new RefreshTokenRepository(context));

            _unitOfWork = mockUnitOfWork.Object;
            _jwtService = mockJwtService.Object;
        }

        [Fact]
        public async Task SignUpAsync_SignUpModel_Tokens()
        {
            var model = new SignUpModel("newLogin", "newPaswword");

            var userService = new UserService(_unitOfWork, _jwtService);

            var response = await userService.SignUpAsync(model);

            Assert.NotNull(response);
            Assert.Equal("newLogin", response.User.Login);
            Assert.Equal("newTestAccessToken", response.TokensModel.AccessToken);
            Assert.Equal("newTestRefreshToken", response.TokensModel.RefreshToken);
            Assert.Equal("newTestRefreshToken", response.User.RefreshToken.Token);
        }
        [Fact]
        public async Task SignUpAsync_ExistsLogin_Null()
        {
            var model = new SignUpModel("test", "testpass");

            var userService = new UserService(_unitOfWork, _jwtService);

            var response = await userService.SignUpAsync(model);

            Assert.Null(response);
        }
        [Fact]
        public async Task SignUpAsync_ArgumentNullException()
        {
            var userService = new UserService(_unitOfWork, _jwtService);

            await Assert.ThrowsAsync<ArgumentNullException>(async () => 
                await userService.SignUpAsync(null));
        }
        [Fact]
        public async Task SignInAsync_ValidModel_Tokens()
        {
            var model = new SignInModel("test2", "testPass2");

            var userService = new UserService(_unitOfWork, _jwtService);

            var response = await userService.SignInAsync(model);

            Assert.NotNull(response);
            Assert.Equal("test2", response.User.Login);
            Assert.Equal("newTestAccessToken", response.TokensModel.AccessToken);
            Assert.Equal("newTestRefreshToken", response.TokensModel.RefreshToken);
            Assert.Equal("newTestRefreshToken", response.User.RefreshToken.Token);
        }
        [Fact]
        public async Task SignInAsync_ValidModelWithExistsRefreshToken_Tokens()
        {
            var model = new SignInModel("test3", "testPass3");

            var userService = new UserService(_unitOfWork, _jwtService);

            var response = await userService.SignInAsync(model);

            Assert.NotNull(response);
            Assert.Equal("test3", response.User.Login);
            Assert.Equal("newTestAccessToken", response.TokensModel.AccessToken);
            Assert.Equal("newTestRefreshToken", response.TokensModel.RefreshToken);
            Assert.Equal("newTestRefreshToken", response.User.RefreshToken.Token);
        }
        [Fact]
        public async Task SignInAsync_NotValid_Null()
        {
            var model = new SignInModel("notValidLogin", "notValidPassword");

            var userService = new UserService(_unitOfWork, _jwtService);

            var response = await userService.SignInAsync(model);

            Assert.Null(response);
        }
        [Fact]
        public async Task SignInAsync_ValidLogin_Null()
        {
            var model = new SignInModel("test2", "notValidPassword");

            var userService = new UserService(_unitOfWork, _jwtService);

            var response = await userService.SignInAsync(model);

            Assert.Null(response);
        }
        [Fact]
        public async Task SignInAsync_ArgumentNullException()
        {
            var userService = new UserService(_unitOfWork, _jwtService);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await userService.SignInAsync(null));
        }
    }
}
