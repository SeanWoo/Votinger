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
    public class GrpcUserService : GrpcUser.GrpcUserBase
    {
        private readonly IUserService _service;
        private readonly ILogger<GrpcUserService> _logger;
        public GrpcUserService(
            IUserService service,
            ILogger<GrpcUserService> logger)
        {
            _service = service;
            _logger = logger;
        }

        public override async Task<GrpcSignReply> SignIn(GrpcSignRequest request, ServerCallContext context)
        {
            _logger.LogDebug("An attempt to log into an account with a login: ", request.Login);

            var signInModel = new SignInModel(request.Login, request.Password);

            var result = await _service.SignInAsync(signInModel);
            
            if (result is null)
            {
                return new GrpcSignReply()
                {
                    Status = ApiStatusTypes.Error,
                    Error = new GrpcError()
                    {
                        StatusCode = (int)ApiErrorStatusEnum.ERROR_NOT_VALID_CREDENTIALS,
                        Message = "Not valid credentials"
                    }
                };
            }

            return new GrpcSignReply()
            {
                Status = ApiStatusTypes.Success,
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
                    Status = ApiStatusTypes.Error,
                    Error = new GrpcError()
                    {
                        StatusCode = (int)ApiErrorStatusEnum.ERROR_NOT_VALID_CREDENTIALS,
                        Message = "Not valid credentials"
                    }
                };
            }

            return new GrpcSignReply()
            {
                Status = ApiStatusTypes.Success,
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
                    Status = ApiStatusTypes.Error,
                    Error = new GrpcError()
                    {
                        StatusCode = (int)ApiErrorStatusEnum.ERROR_NOT_VALID_REFRESH_TOKEN,
                        Message = "Not valid refresh token"
                    }
                };
            }

            return new GrpcSignReply()
            {
                Status = ApiStatusTypes.Success,
                Tokens = new GrpcTokensReply()
                {
                    AccessToken = result.AccessToken,
                    RefreshToken = result.RefreshToken
                }
            };
        }
    }
}
