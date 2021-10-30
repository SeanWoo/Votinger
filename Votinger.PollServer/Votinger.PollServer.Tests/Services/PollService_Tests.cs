using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votinger.PollServer.Core.Entities;
using Votinger.PollServer.Infrastructure.Data;
using Votinger.PollServer.Infrastructure.Repository;
using Votinger.PollServer.Infrastructure.Repository.Entities;
using Votinger.PollServer.Services.Polls;
using Votinger.PollServer.Services.Polls.Model;
using Xunit;

namespace Votinger.PollServer.Tests.Services
{
    public class PollService_Tests
    {
        private IPollService _service;
        private IUnitOfWork _unitOfWork;
        private Poll _testPoll = new Poll()
        {
            Id = 1,
            UserId = 1,
            Title = "test",
            AnswerOptions = new List<PollAnswerOption>()
            {
                new PollAnswerOption()
                {
                    Id = 1,
                    Text = "testAnswer1"
                },
                new PollAnswerOption()
                {
                    Id = 2,
                    Text = "testAnswer2"
                },
                new PollAnswerOption()
                {
                    Id = 3,
                    Text = "testAnswer3"
                },
            }
        };
        public PollService_Tests()
        {
            var options = new DbContextOptionsBuilder<PollServerDatabaseContext>()
                .UseInMemoryDatabase("MockDBPoll")
                .Options;

            var context = new PollServerDatabaseContext(options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            _unitOfWork = new UnitOfWork(context);

            _unitOfWork.Polls.InsertAsync(_testPoll);
            _unitOfWork.SaveChangesAsync();

            _service = new PollService(_unitOfWork);
        }

        [Fact]
        public async Task CreatePoll_CreatePollModel_ExistsInDb()
        {
            var createPollModel = new CreatePollModel()
            {
                UserId = 1,
                Title = "It is test poll",
                AnswerOptions = new List<string>()
                {
                    "Test 1",
                    "Test 2",
                    "Test 3"
                }
            };

            var poll = await _service.CreatePollAsync(createPollModel);

            Assert.NotNull(poll);
            Assert.Equal("It is test poll", poll.Title);
            Assert.Equal("Test 2", poll.AnswerOptions[1].Text);
        }
        [Fact]
        public async Task GetFew_0_2_Polls()
        {
            await _unitOfWork.Polls.InsertAsync(new Poll() { Id = 2, Title = "test 2" });
            await _unitOfWork.Polls.InsertAsync(new Poll() { Id = 3, Title = "test 3" });
            await _unitOfWork.SaveChangesAsync();

            var poll = await _service.GetFew(0, 2);

            Assert.NotNull(poll);
            Assert.Equal(2, poll.Count());
        }
        [Fact]
        public async Task GetFew_5_2_Polls()
        {
            await _unitOfWork.Polls.InsertAsync(new Poll() { Id = 2, Title = "test 2" });
            await _unitOfWork.Polls.InsertAsync(new Poll() { Id = 3, Title = "test 3" });
            await _unitOfWork.SaveChangesAsync();

            var poll = await _service.GetFew(5, 2);

            Assert.NotNull(poll);
            Assert.Empty(poll);
        }
        [Fact]
        public async Task GetPollById_ThreePoll_PollById1()
        {
            await _unitOfWork.Polls.InsertAsync(new Poll() { Id = 2, Title = "test" });
            await _unitOfWork.SaveChangesAsync();

            var poll = await _service.GetPollByIdAsync(2);

            Assert.NotNull(poll);
            Assert.Equal("test", poll.Title);
        }
        [Fact]
        public async Task GetPollByIdWithAnswers_Poll_PollById()
        {
            var poll = await _service.GetPollByIdAsync(1, 
                includeAnswers: true,
                includeRepliedUsers: true);

            Assert.Equal("test", poll.Title);
            Assert.Equal("testAnswer1", poll.AnswerOptions[0].Text);
        }

        [Fact]
        public async Task RemovePoll_PoolById1_Null()
        {
            var poll = new Poll() { Id = 2, Title = "test" };
            await _unitOfWork.Polls.InsertAsync(poll);
            await _unitOfWork.SaveChangesAsync();

            var result = await _service.RemovePollAsync(poll);

            var repositoryResult = await _unitOfWork.Polls.GetByIdAsync(2);

            Assert.True(result);
            Assert.Null(repositoryResult);
        }
        [Fact]
        public async Task VoteInPoll_NullPollId_False()
        {
            var input = new AnswerPollModel()
            {
                UserId = 1,
                PollId = 0,
                AnswerOptionIds = new int[] { 2 }
            };

            var result = await _service.VoteInPollAsync(input);

            Assert.False(result);
        }
        [Fact]
        public async Task VoteInPoll_NullPollAndAnswerId_False()
        {
            var input = new AnswerPollModel()
            {
                UserId = 1,
                PollId = 0,
                AnswerOptionIds = new int[] { 0 }
            };

            var result = await _service.VoteInPollAsync(input);

            Assert.False(result);
        }
        [Fact]
        public async Task VoteInPoll_DoubleVote_TrueFalse()
        {
            var input = new AnswerPollModel()
            {
                UserId = 1,
                PollId = _testPoll.Id,
                AnswerOptionIds = new int[] { 1 }
            };

            var result1 = await _service.VoteInPollAsync(input);
            var result2 = await _service.VoteInPollAsync(input);

            Assert.True(result1);
            Assert.False(result2);
            Assert.Equal(1, _testPoll.AnswerOptions[0].NumberOfReplies);
            Assert.Equal(1, _testPoll.AnswerOptions[0].RepliedUsers[0].UserId);
            Assert.Single(_testPoll.AnswerOptions[0].RepliedUsers);
        }
        [Fact]
        public async Task VoteInPoll_OneVariantVote()
        {
            var input = new AnswerPollModel()
            {
                UserId = 1,
                PollId = _testPoll.Id,
                AnswerOptionIds = new int[] { 2 }
            };

            var result = await _service.VoteInPollAsync(input);

            Assert.True(result);
            Assert.Equal(1, _testPoll.AnswerOptions[1].NumberOfReplies);
            Assert.Equal(1, _testPoll.AnswerOptions[1].RepliedUsers[0].UserId);
        }
        [Fact]
        public async Task VoteInPoll_TwoVariantVote()
        {
            var input = new AnswerPollModel()
            {
                UserId = 1,
                PollId = _testPoll.Id,
                AnswerOptionIds = new int[] { 2, 3 }
            };

            var result = await _service.VoteInPollAsync(input);

            Assert.True(result);
            Assert.Equal(1, _testPoll.AnswerOptions[1].NumberOfReplies);
            Assert.Equal(1, _testPoll.AnswerOptions[2].NumberOfReplies);
            Assert.Equal(1, _testPoll.AnswerOptions[1].RepliedUsers[0].UserId);
            Assert.Equal(1, _testPoll.AnswerOptions[2].RepliedUsers[0].UserId);
        }
        [Fact]
        public async Task VoteInPoll_TwoVariantVote_OneVote()
        {
            var input = new AnswerPollModel()
            {
                UserId = 1,
                PollId = _testPoll.Id,
                AnswerOptionIds = new int[] { 2, 2 }
            };

            var result = await _service.VoteInPollAsync(input);

            Assert.True(result);
            Assert.Equal(1, _testPoll.AnswerOptions[1].NumberOfReplies);
            Assert.Equal(1, _testPoll.AnswerOptions[1].RepliedUsers[0].UserId);
            Assert.Single(_testPoll.AnswerOptions[1].RepliedUsers);
        }
        [Fact]
        public async Task CancelVoteInPoll_Poll_NotVote()
        {
            var replies = new PollRepliedUser[] {
                new PollRepliedUser(){
                    UserId = 1,
                    PollAnswerOption = _testPoll.AnswerOptions[0],
                },
                new PollRepliedUser(){
                    UserId = 1,
                    PollAnswerOption = _testPoll.AnswerOptions[1],
                },
                new PollRepliedUser(){
                    UserId = 2,
                    PollAnswerOption = _testPoll.AnswerOptions[1],
                },
            };
            _testPoll.AnswerOptions[0].NumberOfReplies += 1;
            _testPoll.AnswerOptions[1].NumberOfReplies += 2;
            await _unitOfWork.PollRepliedUsers.InsertAsync(replies);
            _unitOfWork.PollAnswerOptions.Update(_testPoll.AnswerOptions);
            await _unitOfWork.SaveChangesAsync();

            var result = await _service.CancelVoteInPollAsync(1, 1);

            Assert.True(result);
            Assert.Equal(0, _testPoll.AnswerOptions[0].NumberOfReplies);
            Assert.Equal(1, _testPoll.AnswerOptions[1].NumberOfReplies);
            Assert.Empty(_testPoll.AnswerOptions[0].RepliedUsers);
            Assert.Empty(_testPoll.AnswerOptions[1].RepliedUsers);
        }
     }
}
