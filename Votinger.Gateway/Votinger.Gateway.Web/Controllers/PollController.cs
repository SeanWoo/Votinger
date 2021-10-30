using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Votinger.Gateway.Web.Models.Poll;
using Votinger.Protos;

namespace Votinger.Gateway.Web.Controllers
{
    public class PollController : BaseApiController
    {
        private readonly GrpcPollService.GrpcPollServiceClient _client;
        public PollController(GrpcPollService.GrpcPollServiceClient client)
        {
            _client = client;
        }
        [HttpGet]
        public async Task<IActionResult> GetFew([FromQuery] GetFewRequest request)
        {
            var grpcGetFewRequest = new GrpcGetFewRequest();
            grpcGetFewRequest.To = request.To;
            grpcGetFewRequest.From = request.From;
            grpcGetFewRequest.IncludeAnswers = request.IncludeAnswers;

            var response = await _client.GetFewAsync(grpcGetFewRequest);

            return response.ResultCase switch
            {
                GrpcRepeatedPollResponse.ResultOneofCase.Polls => Ok(response.Polls),
                GrpcRepeatedPollResponse.ResultOneofCase.Error => BadRequest(response.Error.StatusCode, response.Error.Message),
                GrpcRepeatedPollResponse.ResultOneofCase.None => Ok(response.Polls)
            };
        }
        [HttpGet]
        public async Task<IActionResult> GetPollById([FromQuery] GetPollByIdRequest request)
        {
            var grpcGetPollByIdRequest = new GrpcGetPollByIdRequest();
            grpcGetPollByIdRequest.PollId = request.PollId;
            grpcGetPollByIdRequest.IncludeAnswers = request.IncludeAnswerOptions;
            grpcGetPollByIdRequest.IncludeRepliedUsers = request.IncludeRepliedUsers;

            var response = await _client.GetPollByIdAsync(grpcGetPollByIdRequest);

            return response.ResultCase switch
            {
                GrpcPollResponse.ResultOneofCase.Poll => Ok(response.Poll),
                GrpcPollResponse.ResultOneofCase.Error => BadRequest(response.Error.StatusCode, response.Error.Message),
                GrpcPollResponse.ResultOneofCase.None => Ok(response.Poll)
            };
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePoll(CreatePollRequest request)
        {
            var grpcCreatePollRequest = new GrpcCreatePollRequest();
            grpcCreatePollRequest.Title = request.Title;
            grpcCreatePollRequest.AnswerOptions.Add(request.AnswerOptions);

            var response = await _client.CreatePollAsync(grpcCreatePollRequest);

            return response.ResultCase switch
            {
                GrpcPollResponse.ResultOneofCase.Poll => Ok(response.Poll),
                GrpcPollResponse.ResultOneofCase.Error => BadRequest(response.Error.StatusCode, response.Error.Message),
                GrpcPollResponse.ResultOneofCase.None => Ok(response.Poll)
            };
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RemovePoll(RemovePollRequest request)
        {
            var grpcRemovePollRequest = new GrpcRemovePollRequest();
            grpcRemovePollRequest.PollId = request.PollId;

            var response = await _client.RemovePollAsync(grpcRemovePollRequest);

            return response.ResultCase switch
            {
                GrpcEmpty.ResultOneofCase.None => Ok(),
                GrpcEmpty.ResultOneofCase.Error => BadRequest(response.Error.StatusCode, response.Error.Message)
            };
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> VoteInPoll(VoteInPollRequest request)
        {
            var grpcVoteInPollRequest = new GrpcVoteInPollRequest();
            grpcVoteInPollRequest.PollId = request.PollId;
            grpcVoteInPollRequest.AnswerOptionIds.Add(request.AnsweredOptions);

            var response = await _client.VoteInPollAsync(grpcVoteInPollRequest);

            return response.ResultCase switch
            {
                GrpcEmpty.ResultOneofCase.None => Ok(),
                GrpcEmpty.ResultOneofCase.Error => BadRequest(response.Error.StatusCode, response.Error.Message)
            };
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CancelVoteInPoll(CancelVoteInPoll request)
        {
            var grpcCancelVoteInPollRequest = new GrpcCancelVoteInPollRequest();
            grpcCancelVoteInPollRequest.PollId = request.PollId;

            var response = await _client.CancelVoteInPollAsync(grpcCancelVoteInPollRequest);

            return response.ResultCase switch
            {
                GrpcEmpty.ResultOneofCase.None => Ok(),
                GrpcEmpty.ResultOneofCase.Error => BadRequest(response.Error.StatusCode, response.Error.Message)
            };
        }
    }
}
