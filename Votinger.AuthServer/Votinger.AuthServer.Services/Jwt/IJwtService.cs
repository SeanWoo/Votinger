using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Votinger.AuthServer.Core.Entities;
using Votinger.AuthServer.Services.Jwt.Models;

namespace Votinger.AuthServer.Services.Jwt
{
    /// <summary>
    /// Service for creating and updating tokens
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        /// Generate access and refresh token
        /// </summary>
        /// <returns></returns>
        TokensModel GenerateTokens(User user);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jwtToken"></param>
        /// <returns></returns>
        ClaimsPrincipal Validate(string jwtToken);
    }
}
