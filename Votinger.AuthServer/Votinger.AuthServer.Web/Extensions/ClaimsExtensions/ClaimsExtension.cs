using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Votinger.AuthServer.Web.Models;

namespace Votinger.AuthServer.Web.Extensions.ClaimsExtensions
{
    public static class ClaimsExtension
    {
        public static JwtClaimsModel GetClaimsModel(this IEnumerable<Claim> claims)
        {
            var jwtClaimModel = new JwtClaimsModel()
            {
                UserId = Convert.ToInt32(claims.FirstOrDefault(x => x.Type == "id")?.Value),
            };
            return jwtClaimModel;
        }
    }
}
