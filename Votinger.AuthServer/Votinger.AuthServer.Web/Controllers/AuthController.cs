using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Votinger.AuthServer.Services.Jwt;
using Votinger.AuthServer.Web.Models.Responses;
using Votinger.AuthServer.Web.Services;

namespace Votinger.AuthServer.Web.Controllers
{
    [ApiController]
    [Route("/api")]
    public class AuthController : ControllerBase
    {
        public readonly IJwtService _jwtService;

        public AuthController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }


        [HttpGet("signin")]
        public async Task<GetTokenResponse> SignIn()
        {
            throw new NotImplementedException();
        }
    }
}
