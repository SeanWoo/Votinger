using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votinger.PollServer.Core.Entities;
using Votinger.PollServer.Services.Mapper;
using Votinger.PollServer.Services.Polls;
using Votinger.PollServer.Services.Polls.Model;
using Votinger.PollServer.Web.GrpcServices;
using Votinger.PollServer.Web.Mapper;
using Votinger.Protos;
using Xunit;
using static Votinger.Protos.GrpcPoll.Types;
using static Votinger.Protos.GrpcPoll.Types.GrpcPollAnswerOption.Types;

namespace Votinger.PollServer.Tests.Services
{
    public class GrpcPollService_Tests
    {
        [Fact]
        public void Test()
        {
            var conf = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = new Mapper(conf);

            var polls = new List<Poll>()
            {
                new Poll()
                {
                    
                }
            };

            var response = mapper.Map<PollAnswerOption, GrpcPollAnswerOption>(new PollAnswerOption()
            {
                IsAnswered = true,
                Id = 1,
                PollId = 2,
                NumberOfReplies = 33,
                RepliedUsers = new List<PollRepliedUser>(),
                Text = "asd"
            });
            //var response = mapper.Map<IEnumerable<Poll>, GrpcRepeatedPoll>(polls);

            Assert.NotNull(response);
        }
    }
}
