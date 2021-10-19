using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Votinger.Gateway.Web.Models.Auth;
using Votinger.Protos;

namespace Votinger.Gateway.Web.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly GrpcUser.GrpcUserClient _userClient;

        public AuthController(GrpcUser.GrpcUserClient userClient)
        {
            _userClient = userClient;
        }

        [HttpPost]
        public async Task<SignResponse> SignIn(SignInModel model)
        {
            var result = await _userClient.SignInAsync(new GrpcSignRequest()
            {
                Login = model.Login,
                Password = model.Password
            });

            switch (result.ResultCase)
            {
                case GrpcSignReply.ResultOneofCase.Error:
                    break;
                case GrpcSignReply.ResultOneofCase.Tokens:
                    return new SignResponse(result.Status, result.Tokens.AccessToken, result.Tokens.RefreshToken);
                default:
                    break;
            }

            return new SignResponse(result.Status, result.AccessToken, result.RefreshToken);
        }

        [HttpPost]
        public async Task<SignResponse> SignUp(SignUpModel model)
        {
            var result = await _userClient.SignUpAsync(new GrpcSignRequest()
            {
                Login = model.Login,
                Password = model.Password
            });

            return new SignResponse(result.Status, result.AccessToken, result.RefreshToken);
        }
    }
}
