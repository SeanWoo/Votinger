using Microsoft.Extensions.DependencyInjection;
using Votinger.PollServer.Infrastructure.Data;
using Votinger.PollServer.Infrastructure.Repository;
using Votinger.PollServer.Services.Polls;

namespace Votinger.PollServer.Web.Extensions.IoCExtensions
{
    public static class ServiceExtention
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<PollServerDatabaseContext>();

            services.AddTransient<IPollService, PollService>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
