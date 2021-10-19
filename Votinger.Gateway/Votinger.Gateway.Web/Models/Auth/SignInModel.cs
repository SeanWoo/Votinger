using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Votinger.Gateway.Web.Models.Auth
{
    public record SignInModel(string Login, string Password);
}
