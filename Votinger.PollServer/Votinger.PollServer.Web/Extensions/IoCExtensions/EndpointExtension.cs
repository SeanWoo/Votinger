using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Votinger.PollServer.Web.GrpcServices;

namespace Votinger.PollServer.Web.Extensions.IoCExtensions
{
    public static class EndpointExtension
    {
        public static IApplicationBuilder UseEndpointsMiddleware(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<TestService>();
                endpoints.MapGrpcService<GrpcPollServiceImplementation>();
            });

            return app;
        }
    }
}
