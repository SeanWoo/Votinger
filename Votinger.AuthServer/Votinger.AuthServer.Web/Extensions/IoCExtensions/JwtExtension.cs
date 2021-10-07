using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Votinger.AuthServer.Services.Jwt;
using Votinger.AuthServer.Web.Services;

namespace Votinger.AuthServer.Web.Extensions.IoCExtensions
{
    /// <summary>
    /// Configures authentication and authorization for JWT
    /// </summary>
    public static class JwtExtension
    {
        /// <summary>
        /// Configures authentication and authorization for JWT
        /// </summary>
        public static IServiceCollection AddJwtAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.RequireHttpsMetadata = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = configuration["JwtOptions:Issuer"],

                        ValidateAudience = true,
                        ValidAudience = configuration["JwtOptions:Audience"],
                        ValidateLifetime = true,

                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JwtOptions:Secret"])),
                        ValidateIssuerSigningKey = true,
                    };
                });

            services.AddAuthorization(options =>
            {
                var policyBuilder = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
                policyBuilder.RequireClaim("id");
                policyBuilder.RequireAuthenticatedUser();
                options.DefaultPolicy = policyBuilder.Build();

                //options.AddPolicy("Moderation", policy => policy.RequireAuthenticatedUser().RequireClaim()
            });

            services.AddTransient<IJwtService, JwtService>();

            return services;
        }
    }
}
