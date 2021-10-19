using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Votinger.AuthServer.Services.Users;
using Votinger.AuthServer.Services.Users.Models;
using Votinger.Protos;

namespace Votinger.AuthServer.Web.GrpcServices
{
    public class GrpcUserService : GrpcUser.GrpcUserBase
    {
        private readonly IUserService _service;
        public GrpcUserService(IUserService service)
        {
            _service = service;
        }

        public override async Task<GrpcSignReply> SignIn(GrpcSignRequest request, ServerCallContext context)
        {
            var signInModel = new SignInModel(request.Login, request.Password);

            var result = await _service.SignInAsync(signInModel);
            
            if (result is null)
            {
                return new GrpcSignReply()
                {
                    Status = "ERROR",
                    
                    Error = new GrpcError()
                    {
                        StatusCode = "100",
                        Message = "error"
                    }
                };
            }

            return new GrpcSignReply()
            {
                Status = "DONE",
                Tokens = new GrpcTokensReply()
                {
                    AccessToken = result.TokensModel.AccessToken,
                    RefreshToken = result.TokensModel.RefreshToken
                }
            };
        }

        public override async Task<GrpcSignReply> SignUp(GrpcSignRequest request, ServerCallContext context)
        {
            var signInModel = new SignInModel(request.Login, request.Password);

            var result = await _service.SignInAsync(signInModel);

            if (result is null)
            {
                return new GrpcSignReply()
                {
                    Status = "ERROR",
                    
                    Error = new GrpcError()
                    {
                        StatusCode = "100",
                        Message = "error"
                    }
                };
            }

            return new GrpcSignReply()
            {
                Status = "DONE",
                Tokens = new GrpcTokensReply()
                {
                    AccessToken = result.TokensModel.AccessToken,
                    RefreshToken = result.TokensModel.RefreshToken
                }
            };
        }
    }
}
