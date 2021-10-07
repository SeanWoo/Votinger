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
    public class GetRepositoryTests
    {
        private IRepository<User> _userRepository;

        public GetRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AuthServerDatabaseContext>()
                .UseInMemoryDatabase("MockDBGet")
                .Options;

            var context = new AuthServerDatabaseContext(options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Users.Add(new() { Login = "test" });
            context.Users.Add(new() { Login = "test2" });
            context.Users.Add(new() { Login = "test3" });
            context.SaveChanges();

            _userRepository = new EntityRepository<User, AuthServerDatabaseContext>(context);
        }

        [Fact]
        public void GetById_Index0_Null()
        {
            var user = _userRepository.GetById(0);

            Assert.Null(user);
        }
        [Fact]
        public void GetById_Index2_test2()
        {
            var user = _userRepository.GetById(2);

            Assert.Equal("test2", user.Login);
        }
        [Fact]
        public async Task GetByIdAsync_Index0_Null()
        {
            var user = await _userRepository.GetByIdAsync(0);

            Assert.Null(user);
        }
        [Fact]
        public async Task GetByIdAsync_Index2_test2()
        {
            var user = await _userRepository.GetByIdAsync(2);

            Assert.Equal("test2", user.Login);
        }

        [Fact]
        public void GetAll_Count3()
        {
            var userList = _userRepository.GetAll();

            Assert.Equal(3, userList.Count());
        }
        [Fact]
        public async Task GetAllAsync_Count3()
        {
            var userList = await _userRepository.GetAllAsync();

            Assert.Equal(3, userList.Count());
        }

        [Fact]
        public void GetAll_Filter_Count2()
        {
            var userList = _userRepository.GetAll(x => x.Login == "test2" || x.Login == "test3");

            Assert.Equal(2, userList.Count());
        }
        [Fact]
        public async Task GetAllAsync_Filter_Count2()
        {
            var userList = await _userRepository.GetAllAsync(x => x.Login == "test2" || x.Login == "test3");

            Assert.Equal(2, userList.Count());
        }
    }
}
