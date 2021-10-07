using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votinger.AuthServer.Core.Entities;
using Votinger.AuthServer.Infrastructure.Data;
using Votinger.AuthServer.Infrastructure.Repository;
using Votinger.AuthServer.Infrastructure.Repository.Entities;
using Xunit;

namespace Votinger.AuthServer.Tests.Infrastructure.Repository
{
    public class UpdateRepositoryTest
    {
        private IRepository<User> _userRepository;
        private User testUser;

        public UpdateRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<AuthServerDatabaseContext>()
                .UseInMemoryDatabase("MockDBUpdate")
                .Options;

            var context = new AuthServerDatabaseContext(options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            testUser = new User() // ID: 4
            {
                Login = "test4"
            };

            context.Users.Add(new() { Login = "test" });
            context.Users.Add(new() { Login = "test2" });
            context.Users.Add(new() { Login = "test3" });
            context.Users.Add(testUser);

            context.SaveChanges();

            _userRepository = new EntityRepository<User, AuthServerDatabaseContext>(context);
        }

        [Fact]
        public void Update_User_testUpdate()
        {
            var user = _userRepository.GetById(4);

            //Operation
            user.Login = "testUpdate";
            _userRepository.Update(user);

            //Assert
            var result = _userRepository.GetById(4);

            Assert.Equal("testUpdate", result.Login);
        }
        [Fact]
        public async Task UpdateAsync_User_testUpdate()
        {
            var user = _userRepository.GetById(4);

            //Operation
            user.Login = "testUpdate";
            await _userRepository.UpdateAsync(user);

            //Assert
            var result = await _userRepository.GetByIdAsync(4);

            Assert.Equal("testUpdate", result.Login);
        }
    }
}
