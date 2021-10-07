using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Votinger.AuthServer.Core.Entities;
using Votinger.AuthServer.Services.Jwt;
using Votinger.AuthServer.Services.Jwt.Models;

namespace Votinger.AuthServer.Web.Services
{
    public class JwtService : IJwtService
    {
        public readonly IConfiguration _configuration;
        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TokensModel GenerateTokens(User user)
        {
            var accessToken = GenerateAccessToken(user);
            var refreshToken = GenerateRefreshToken(user);

            return new (accessToken, refreshToken);
        }

        public ClaimsPrincipal Validate(string jwtToken)
        {
            ClaimsPrincipal claimPrincipal;
            var handler = new JwtSecurityTokenHandler();

            try
            {
                claimPrincipal = handler.ValidateToken(jwtToken, new()
                {
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["JwtOptions:Issuer"],

                    ValidateAudience = true,
                    ValidAudience = _configuration["JwtOptions:Audience"],
                    ValidateLifetime = true,

                    IssuerSigningKey = GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                }, out _);
            }
            catch (Exception)
            {
                return null;
            }

            return claimPrincipal;
        }

        private string GenerateAccessToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim("id", user.Id.ToString())
            };

            var claimIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            var now = DateTime.UtcNow;

            var jwtToken = new JwtSecurityToken(
                issuer: _configuration["JwtOptions:Issuer"],
                audience: _configuration["JwtOptions:Audience"],
                notBefore: now,
                claims: claimIdentity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(Convert.ToDouble(_configuration["JwtOptions:Lifetime"]))),
                signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return accessToken;
        }

        private string GenerateRefreshToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim("id", user.Id.ToString())
            };

            var claimIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            var now = DateTime.UtcNow;

            var jwtToken = new JwtSecurityToken(
                issuer: _configuration["JwtOptions:Issuer"],
                audience: _configuration["JwtOptions:Audience"],
                notBefore: now,
                claims: claimIdentity.Claims,
                expires: now.AddYears(10),
                signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            );

            var refreshToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return refreshToken;
        }

        private SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JwtOptions:Secret"]));
        }
    }
}
