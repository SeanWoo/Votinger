using Microsoft.Extensions.DependencyInjection;
using Votinger.AuthServer.Infrastructure.Repository;
using Votinger.AuthServer.Infrastructure.Repository.Entities;
using Votinger.AuthServer.Infrastructure.Repository.Entities.Interfaces;
using Votinger.AuthServer.Services.Users;

namespace Votinger.AuthServer.Web.Extensions.IoCExtensions
{
    public static class ServiceExtention
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();

            //Repositories
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserClaimRepository, UserClaimRepository>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();

            return services;
        }
    }
}
