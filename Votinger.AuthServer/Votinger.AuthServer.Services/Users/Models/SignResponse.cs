using Votinger.AuthServer.Core.Entities;
using Votinger.AuthServer.Services.Jwt.Models;

namespace Votinger.AuthServer.Services.Users.Models
{
    public record SignResponse(
        User User, 
        TokensModel TokensModel);
}
