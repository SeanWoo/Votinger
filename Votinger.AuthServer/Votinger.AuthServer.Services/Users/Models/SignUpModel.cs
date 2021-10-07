using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Votinger.AuthServer.Services.Users.Models
{
    public record SignUpModel (
        string Login,
        string Password
        );
}
