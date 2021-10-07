using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using Votinger.AuthServer.Services.Users;
using Votinger.AuthServer.Web.Extensions.ClaimsExtensions;
using Votinger.AuthServer.Web.Models.Requests;

namespace Votinger.AuthServer.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IUserService _userService;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger, 
            IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        public string Get()
        {
            return $"Name: {HttpContext.User.Identity.Name} IsAuth: {HttpContext.User.Identity.IsAuthenticated} ID: {HttpContext.User.Claims.GetClaimsModel().UserId}";
        }

        [HttpGet("2")]
        public string Get2()
        {
            return $"Name: {HttpContext.User.Identity.Name} IsAuth: {HttpContext.User.Identity.IsAuthenticated}";
        }

        [HttpPost("3")]
        public IActionResult Get3(SignInRequest request)
        {
            ModelState.AddModelError("hi", "asf");
            ModelState.AddModelError("hi", "asf2");
            ModelState.AddModelError("hi2", "123");
            return BadRequest();
        }
    }
}
