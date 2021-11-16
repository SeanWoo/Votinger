using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votinger.PollServer.Core.Entities;
using Votinger.PollServer.Services.Polls.Model;

namespace Votinger.PollServer.Services.Mapper
{
    public class PollProfile : Profile
    {
        public PollProfile()
        {
            CreateMap<PollAnswerOption, PollAnswerOptionModel>().ReverseMap();
            CreateMap<Poll, PollModel>().ReverseMap().ForMember(x => x.AnswerOptions, opt => opt.MapFrom(x => x));
        }
    }
}
