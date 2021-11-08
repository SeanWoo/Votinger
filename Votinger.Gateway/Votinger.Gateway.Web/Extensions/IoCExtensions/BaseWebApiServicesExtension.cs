using Microsoft.AspNetCore.Builder;
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
            services.AddCors();
            services.AddGrpc();

            return services;
        }
        public static IApplicationBuilder UseBaseWebApiServices(this IApplicationBuilder app)
        {
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader());

            return app;
        }
    }
}
