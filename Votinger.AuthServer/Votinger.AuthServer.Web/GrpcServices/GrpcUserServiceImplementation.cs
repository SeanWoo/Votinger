using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Votinger.AuthServer.Core;
using Votinger.AuthServer.Core.Enums;
using Votinger.AuthServer.Services.Users;
using Votinger.AuthServer.Services.Users.Models;
using Votinger.AuthServer.Web.Models;
using Votinger.Protos;

namespace Votinger.AuthServer.Web.GrpcServices
{
    public class GrpcUserServiceImplementation : GrpcUserService.GrpcUserServiceBase
    {
        private readonly IUserService _service;
        private readonly ILogger<GrpcUserServiceImplementation> _logger;
        public GrpcUserServiceImplementation(
            IUserService service,
            ILogger<GrpcUserServiceImplementation> logger)
        {
            _service = service;
            _logger = logger;
        }

        public override async Task<GrpcSignReply> SignIn(GrpcSignRequest request, ServerCallContext context)
        {
            var signInModel = new SignInModel(request.Login, request.Password);

            var result = await _service.SignInAsync(signInModel);
            
            if (result is null)
            {
                return new GrpcSignReply()
                {
                    Error = new GrpcError()
                    {
                        StatusCode = (int)ApiErrorStatusEnum.ERROR_NOT_VALID_CREDENTIALS,
                        Message = "Not valid credentials"
                    }
                };
            }

            return new GrpcSignReply()
            {
                Tokens = new GrpcTokensReply()
                {
                    AccessToken = result.TokensModel.AccessToken,
                    RefreshToken = result.TokensModel.RefreshToken
                }
            };
        }

        public override async Task<GrpcSignReply> SignUp(GrpcSignRequest request, ServerCallContext context)
        {
            var signUpModel = new SignUpModel(request.Login, request.Password);

            var result = await _service.SignUpAsync(signUpModel);

            if (result is null)
            {
                return new GrpcSignReply()
                {
                    Error = new GrpcError()
                    {
                        StatusCode = (int)ApiErrorStatusEnum.ERROR_LOGIN_IS_BUSY,
                        Message = "Login already exists"
                    }
                };
            }

            return new GrpcSignReply()
            {
                Tokens = new GrpcTokensReply()
                {
                    AccessToken = result.TokensModel.AccessToken,
                    RefreshToken = result.TokensModel.RefreshToken
                }
            };
        }

        public override async Task<GrpcSignReply> RefreshToken(GrpcRefreshRequest request, ServerCallContext context)
        {
            var result = await _service.RefreshTokenAsync(request.RefreshToken);

            if (result is null)
            {
                return new GrpcSignReply()
                {
                    Error = new GrpcError()
                    {
                        StatusCode = (int)ApiErrorStatusEnum.ERROR_NOT_VALID_REFRESH_TOKEN,
                        Message = "Not valid refresh token"
                    }
                };
            }

            return new GrpcSignReply()
            {
                Tokens = new GrpcTokensReply()
                {
                    AccessToken = result.AccessToken,
                    RefreshToken = result.RefreshToken
                }
            };
        }
    }
}
