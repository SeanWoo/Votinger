using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Votinger.PollServer.Web.Extensions.IoCExtensions
{
    public static class EndpointExtension
    {
        public static IApplicationBuilder UseEndpointsMiddleware(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            return app;
        }
    }
}
