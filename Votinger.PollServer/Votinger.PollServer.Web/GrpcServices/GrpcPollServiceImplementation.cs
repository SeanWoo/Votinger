using AutoMapper;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Votinger.PollServer.Core.Entities;
using Votinger.PollServer.Core.Enums;
using Votinger.PollServer.Services.Polls;
using Votinger.PollServer.Services.Polls.Model;
using Votinger.PollServer.Web.Extensions.ClaimsExtensions;
using Votinger.Protos;

namespace Votinger.PollServer.Web.GrpcServices
{
    [Authorize]
    public class GrpcPollServiceImplementation : GrpcPollService.GrpcPollServiceBase
    {
        private readonly IPollService _pollService;
        private readonly IMapper _mapper;

        public GrpcPollServiceImplementation(
            IPollService pollService,
            IMapper mapper)
        {
            _pollService = pollService;
            _mapper = mapper;
        }
        [AllowAnonymous]
        public override async Task<GrpcRepeatedPollResponse> GetFew(GrpcGetFewRequest request, ServerCallContext context)
        {
            var polls = await _pollService.GetFew(request.From, request.To, request.IncludeAnswers);

            var response = _mapper.Map<GrpcRepeatedPoll>(polls);

            return new GrpcRepeatedPollResponse()
            {
                Polls = response
            };
        }

        [AllowAnonymous]
        public override async Task<GrpcPollResponse> GetPollById(GrpcGetPollByIdRequest request, ServerCallContext context)
        {
            var poll = await _pollService.GetPollByIdAsync(request.PollId,
                includeAnswers: request.IncludeAnswers,
                includeRepliedUsers: request.IncludeRepliedUsers);

            var response = _mapper.Map<GrpcPoll>(poll);

            return new GrpcPollResponse()
            {
                Poll = response
            };
        }
        public override async Task<GrpcPollResponse> CreatePoll(GrpcCreatePollRequest request, ServerCallContext context)
        {
            var claims = context.GetHttpContext().User.Claims.GetClaimsModel();
            var input = _mapper.Map<CreatePollModel>(request);
            input.UserId = claims.UserId;

            var responseService = await _pollService.CreatePollAsync(input);
            var response = _mapper.Map<GrpcPoll>(responseService);

            return new GrpcPollResponse()
            {
                Poll = response
            };
        }
        public override async Task<GrpcEmpty> RemovePoll(GrpcRemovePollRequest request, ServerCallContext context)
        {
            var claims = context.GetHttpContext().User.Claims.GetClaimsModel();
            var poll = new Poll()
            {
                Id = request.PollId,
                UserId = claims.UserId
            };
            var result = await _pollService.RemovePollAsync(poll);

            if (result)
            {
                return new GrpcEmpty();
            }
            else
            {
                return new GrpcEmpty()
                {
                    Error = new GrpcError()
                    {
                        StatusCode = (int)ApiErrorStatusEnum.ERROR_BAD_REQUEST,
                        Message = "Poll does not exist"
                    }
                };
            }
        }
        public override async Task<GrpcEmpty> VoteInPoll(GrpcVoteInPollRequest request, ServerCallContext context)
        {
            var claims = context.GetHttpContext().User.Claims.GetClaimsModel();
            var input = _mapper.Map<AnswerPollModel>(request);
            input.UserId = claims.UserId;

            var result = await _pollService.VoteInPollAsync(input);

            if (result)
            {
                return new GrpcEmpty();
            }
            else
            {
                return new GrpcEmpty()
                {
                    Error = new GrpcError()
                    {
                        StatusCode = (int)ApiErrorStatusEnum.ERROR_BAD_REQUEST,
                        Message = "Poll or PollAnswerOption does not exist"
                    }
                };
            }
        }
        public override async Task<GrpcEmpty> CancelVoteInPoll(GrpcCancelVoteInPollRequest request, ServerCallContext context)
        {
            var claims = context.GetHttpContext().User.Claims.GetClaimsModel();

            var result = await _pollService.CancelVoteInPollAsync(claims.UserId, request.PollId);

            if (result)
            {
                return new GrpcEmpty();
            }
            else
            {
                return new GrpcEmpty()
                {
                    Error = new GrpcError()
                    {
                        StatusCode = (int)ApiErrorStatusEnum.ERROR_BAD_REQUEST,
                        Message = "Poll does not exist"
                    }
                };
            }
        }
    }
}
