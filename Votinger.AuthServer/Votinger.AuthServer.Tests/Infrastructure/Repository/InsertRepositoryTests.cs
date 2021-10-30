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
using Xunit;

namespace Votinger.AuthServer.Tests.Infrastructure.Repository
{
    public class InsertRepositoryTest
    {
        private IRepository<User> _userRepository;

        public InsertRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<AuthServerDatabaseContext>()
                .UseInMemoryDatabase("MockDBInsert")
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
        public void Insert_User_testInsert()
        {
            var user = new User() // ID = 4
            {
                Login = "testInsert"
            };

            //Operation
            _userRepository.Insert(user);

            //Assert
            var result = _userRepository.GetById(4);

            Assert.Equal("testInsert", result.Login);
        }
        [Fact]
        public async Task InsertAsync_User_testInsert()
        {
            var user = new User() // ID = 4
            {
                Login = "testInsert"
            };

            //Operation
            await _userRepository.InsertAsync(user);

            //Assert
            var result = await _userRepository.GetByIdAsync(4);

            Assert.Equal("testInsert", result.Login);
        }
        [Fact]
        public void InsertAll_Users_Count6()
        {
            var users = new List<User>()
            {
                new User() // ID = 4
                {
                    Login = "testInsert1"
                },
                new User() // ID = 5
                {
                    Login = "testInsert2"
                },
                new User() // ID = 6
                {
                    Login = "testInsert3"
                },
            };


            //Operation
            _userRepository.InsertAll(users);

            //Assert
            var result = _userRepository.GetAll();

            Assert.Equal(6, result.Count());
        }
        [Fact]
        public async Task InsertAllAsync_Users_Count6()
        {
            var users = new List<User>()
            {
                new User() // ID = 4
                {
                    Login = "testInsert1"
                },
                new User() // ID = 5
                {
                    Login = "testInsert2"
                },
                new User() // ID = 6
                {
                    Login = "testInsert3"
                },
            };


            //Operation
            await _userRepository.InsertAllAsync(users);

            //Assert
            var result = await _userRepository.GetAllAsync();

            Assert.Equal(6, result.Count());
        }
    }
}
