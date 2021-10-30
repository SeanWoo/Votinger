using Microsoft.AspNetCore.Builder;
using Votinger.AuthServer.Web.GrpcServices;

namespace Votinger.AuthServer.Web.Extensions.IoCExtensions
{
    public static class EndpointExtension
    {
        public static IApplicationBuilder UseEndpointsMiddleware(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GrpcUserServiceImplementation>();
            });

            return app;
        }
    }
}
