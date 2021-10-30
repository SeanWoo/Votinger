using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace Votinger.Gateway.Web.Extensions.IoCExtensions
{
    public static class BaseWebApiServicesExtension
    {
        public static IServiceCollection AddBaseWebApiServices(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddHttpContextAccessor();
            services.AddGrpc();

            return services;
        }
    }
}
