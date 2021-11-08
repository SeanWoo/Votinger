using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Votinger.Gateway.Models;
using Votinger.Gateway.Web.Models;

namespace Votinger.Gateway.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BaseApiController : ControllerBase
    {
        protected BadRequestObjectResult BadRequest(int statusCode, string message)
        {
            return base.BadRequest(new ApiError(statusCode, message));
        }
    }
}
