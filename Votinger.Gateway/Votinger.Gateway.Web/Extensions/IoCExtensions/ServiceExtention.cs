using Microsoft.Extensions.DependencyInjection;
using Votinger.Gateway.Web.Services;

namespace Votinger.Gateway.Web.Extensions.IoCExtensions
{
    public static class ServiceExtention
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<TestService>();

            return services;
        }
    }
}
