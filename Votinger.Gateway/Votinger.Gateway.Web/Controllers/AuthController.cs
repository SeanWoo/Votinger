using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Votinger.Gateway.Web.Models.Auth;
using Votinger.Protos;

namespace Votinger.Gateway.Web.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly GrpcUser.GrpcUserClient _userClient;

        public AuthController(GrpcUser.GrpcUserClient userClient)
        {
            _userClient = userClient;
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInModel model)
        {
            var result = await _userClient.SignInAsync(new GrpcSignRequest()
            {
                Login = model.Login,
                Password = model.Password
            });

            return result.ResultCase switch
            {
                GrpcSignReply.ResultOneofCase.Tokens => Ok(new SignResponse(result.Status, result.Tokens.AccessToken, result.Tokens.RefreshToken)),
                _ => BadRequest(result.Error.StatusCode, result.Error.Message)
            };
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            var result = await _userClient.SignUpAsync(new GrpcSignRequest()
            {
                Login = model.Login,
                Password = model.Password
            });

            return result.ResultCase switch
            {
                GrpcSignReply.ResultOneofCase.Tokens => Ok(new SignResponse(result.Status, result.Tokens.AccessToken, result.Tokens.RefreshToken)),
                _ => BadRequest(result.Error.StatusCode, result.Error.Message)
            };
        }

        [HttpGet]
        public async Task<IActionResult> RefreshTokens(string refreshToken)
        {
            var result = await _userClient.RefreshTokenAsync(new GrpcRefreshRequest()
            {
                RefreshToken = refreshToken
            });

            return result.ResultCase switch
            {
                GrpcSignReply.ResultOneofCase.Tokens => Ok(new SignResponse(result.Status, result.Tokens.AccessToken, result.Tokens.RefreshToken)),
                _ => BadRequest(result.Error.StatusCode, result.Error.Message)
            };
        }
    }
}
