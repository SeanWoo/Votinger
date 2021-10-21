using Microsoft.Extensions.DependencyInjection;
using Votinger.PollServer.Infrastructure.Repository;
using Votinger.PollServer.Infrastructure.Repository.Interfaces;
using Votinger.PollServer.Services.Polls;

namespace Votinger.PollServer.Web.Extensions.IoCExtensions
{
    public static class ServiceExtention
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IPollService, PollService>();

            services.AddTransient<IPollRepository, PollRepository>();

            return services;
        }
    }
}
