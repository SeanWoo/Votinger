using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Votinger.PollServer.Infrastructure.Data;

namespace Votinger.PollServer.Web.Extensions.IoCExtensions
{
    public static class DatabaseExtension
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<PollServerDatabaseContext>(options => 
                options.UseMySql(
                    connectString, 
                    ServerVersion.AutoDetect(connectString)
                )
            );

            return services;
        }
    }
}
