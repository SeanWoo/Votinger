using AutoMapper;
using Google.Protobuf.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Votinger.PollServer.Core.Entities;
using Votinger.PollServer.Services.Polls.Model;
using Votinger.Protos;
using static Votinger.Protos.GrpcPoll.Types;
using static Votinger.Protos.GrpcPoll.Types.GrpcPollAnswerOption.Types;

namespace Votinger.PollServer.Web.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PollRepliedUser, GrpcPollRepliedUser>().ReverseMap();
            CreateMap<PollAnswerOption, GrpcPollAnswerOption>().ReverseMap();
            CreateMap<Poll, GrpcPoll>().ReverseMap();
            CreateMap<IEnumerable<Poll>, GrpcRepeatedPoll>().ForMember(x => x.Polls, opt => opt.MapFrom(t => t));

            CreateMap<CreatePollModel, GrpcCreatePollRequest>().ReverseMap();
            CreateMap<AnswerPollModel, GrpcVoteInPollRequest>().ReverseMap();
        }
    }
}
