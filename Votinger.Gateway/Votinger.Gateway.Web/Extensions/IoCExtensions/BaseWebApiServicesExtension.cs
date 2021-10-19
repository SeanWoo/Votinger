using Microsoft.Extensions.DependencyInjection;

namespace Votinger.Gateway.Web.Extensions.IoCExtensions
{
    public static class BaseWebApiServicesExtension
    {
        public static IServiceCollection AddBaseWebApiServices(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddGrpc();

            return services;
        }
    }
}
