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
        public override OkObjectResult Ok([ActionResultObjectValue] object value)
        {
            return base.Ok(new ApiBaseResponse(ApiStatusTypes.Success, value));
        }
        protected BadRequestObjectResult BadRequest(int statusCode, string message)
        {
            return base.BadRequest(new ApiBaseResponse(ApiStatusTypes.Error, new ApiError(statusCode, message)));
        }
    }
}
