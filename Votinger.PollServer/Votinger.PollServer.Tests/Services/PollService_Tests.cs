using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votinger.PollServer.Core.Entities;
using Votinger.PollServer.Infrastructure.Repository.Interfaces;
using Votinger.PollServer.Services.Polls;
using Votinger.PollServer.Services.Polls.Model;
using Votinger.PollServer.Tests.MockRepositories;
using Xunit;

namespace Votinger.PollServer.Tests.Services
{
    public class PollService_Tests
    {
        private IPollService _service;
        private IPollRepository _pollRepository;
        private Poll _testPoll = new Poll()
        {
            Id = 1,
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
            _pollRepository = new MockPollRepository();

            _service = new PollService(_pollRepository);
        }

        [Fact]
        public async Task CreatePoll_CreatePollModel_ExistsInDb()
        {
            var createPollModel = new CreatePollModel()
            {
                Title = "It is test poll",
                AnswerOptions = new List<string>()
                {
                    "Test 1",
                    "Test 2",
                    "Test 3"
                }
            };

            await _service.CreatePollAsync(createPollModel);

            var poll = await _pollRepository.GetByIdAsync(0);

            Assert.NotNull(poll);
            Assert.Equal("It is test poll", poll.Title);
            Assert.Equal("Test 2", poll.AnswerOptions[1].Text);
        }

        [Fact]
        public async Task GetPollById_ThreePoll_PollById1()
        {
            await _pollRepository.InsertAsync(new Poll() { Id = 0 });
            await _pollRepository.InsertAsync(new Poll() { Id = 1, Title = "test" });
            await _pollRepository.InsertAsync(new Poll() { Id = 2 });

            var poll = await _service.GetPollByIdAsync(1);

            Assert.Equal("test", poll.Title);
        }

        [Fact]
        public async Task RemovePoll_PoolById1_Null()
        {
            var poll2 = new Poll() { Id = 1, Title = "test" };
            await _pollRepository.InsertAsync(new Poll() { Id = 0 });
            await _pollRepository.InsertAsync(poll2);
            await _pollRepository.InsertAsync(new Poll() { Id = 2 });

            await _service.RemovePollAsync(poll2);

            var repositoryResult = await _pollRepository.GetByIdAsync(1);
            Assert.Null(repositoryResult);
        }
        [Fact]
        public async Task VoteInPoll_OneVariantVote()
        {
            await _pollRepository.InsertAsync(_testPoll);

            var input = new AnswerPollModel()
            {
                PollId = _testPoll.Id,
                AnswerOptionIds = new int[] { 2 }
            };
            await _service.VoteInPollAsync(input);

            Assert.Equal(1, _testPoll.AnswerOptions[1].NumberOfReplies);
        }
        [Fact]
        public async Task VoteInPoll_TwoVariantVote()
        {
            await _pollRepository.InsertAsync(_testPoll);

            var input = new AnswerPollModel()
            {
                PollId = _testPoll.Id,
                AnswerOptionIds = new int[] { 2, 3 }
            };
            await _service.VoteInPollAsync(input);

            Assert.Equal(1, _testPoll.AnswerOptions[1].NumberOfReplies);
            Assert.Equal(1, _testPoll.AnswerOptions[2].NumberOfReplies);
        }
        [Fact]
        public async Task VoteInPoll_TwoVariantVote_OneVote()
        {
            await _pollRepository.InsertAsync(_testPoll);

            var input = new AnswerPollModel()
            {
                PollId = _testPoll.Id,
                AnswerOptionIds = new int[] { 2, 2 }
            };
            await _service.VoteInPollAsync(input);

            Assert.Equal(1, _testPoll.AnswerOptions[1].NumberOfReplies);
        }
    }
}
