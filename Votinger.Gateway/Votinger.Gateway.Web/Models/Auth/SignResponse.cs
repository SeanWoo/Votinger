using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Votinger.Gateway.Web.Models.Auth
{
    public record SignResponse(string Status, string AccessToken, string RefreshToken);
}
