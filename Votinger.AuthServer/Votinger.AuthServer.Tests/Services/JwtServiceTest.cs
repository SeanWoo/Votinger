using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using Votinger.AuthServer.Core.Entities;
using Votinger.AuthServer.Web.Services;
using Xunit;

namespace Votinger.AuthServer.Tests.Services
{
    public class JwtServiceTest
    {
        //jwt.io ID: 1
        const string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYXNkIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiYWRtaW4iLCJuYmYiOjE2MzIzMzI0MzQsImlkIjoiMSIsImV4cCI6MTkzMjM1NDAzNCwiaXNzIjoiVGVzdElzc3VlciIsImF1ZCI6IlRlc3RBdWRpZW5jZSJ9.Ssq4AvXTtBXpQQ1UaNRWWWIhCst1LgUO0jA0EyWnAVI";

        public IConfiguration Configuration { get; set; }
        public JwtServiceTest()
        {
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(x => x[It.Is<string>(k => k == "JwtOptions:Issuer")]).Returns("TestIssuer");
            mockConfig.Setup(x => x[It.Is<string>(k => k == "JwtOptions:Audience")]).Returns("TestAudience");
            mockConfig.Setup(x => x[It.Is<string>(k => k == "JwtOptions:Lifetime")]).Returns("100");
            mockConfig.Setup(x => x[It.Is<string>(k => k == "JwtOptions:Secret")]).Returns("mytest_secretkey");

            Configuration = mockConfig.Object;
        }

        [Fact]
        public void GenerateTokens_User_NotEmpty()
        {
            var user = new User()
            {
                Id = 1,
                Login = "testLogin"
            };

            var service = new JwtService(Configuration);

            var results = service.GenerateTokens(user);

            Assert.NotEmpty(results.AccessToken);
            Assert.NotEmpty(results.RefreshToken);
        }

        [Fact]
        public void GenerateTokens_UserId1423_Id1423()
        {
            var user = new User()
            {
                Id = 1423,
                Login = "testLogin"
            };

            var service = new JwtService(Configuration);

            var results = service.GenerateTokens(user);

            var handler = new JwtSecurityTokenHandler();

            var claimAccess = handler.ReadJwtToken(results.AccessToken).Claims.FirstOrDefault(x => x.Type == "id");
            var claimRefresh = handler.ReadJwtToken(results.RefreshToken).Claims.FirstOrDefault(x => x.Type == "id");

            Assert.Contains(user.Id.ToString(), claimAccess.Value);
            Assert.Contains(user.Id.ToString(), claimRefresh.Value);
        }

        [Fact]
        public void Validate_Token_Null()
        {
            var service = new JwtService(Configuration);

            var results = service.Validate("test Token(no)");

            Assert.Null(results);
        }

        [Fact]
        public void Validate_Token_Claims()
        {
            var service = new JwtService(Configuration);

            var results = service.Validate(token);

            Assert.NotNull(results);
            Assert.Equal("1", results.Claims.FirstOrDefault(x => x.Type == "id")?.Value);
        }
    }
}
