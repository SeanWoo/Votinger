using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Votinger.AuthServer.Services.Jwt.Models
{
    public record TokensModel (
        string AccessToken, 
        string RefreshToken);
}
