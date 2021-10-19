﻿using Microsoft.Extensions.DependencyInjection;

namespace Votinger.PollServer.Web.Extensions.IoCExtensions
{
    public static class BaseWebApiServicesExtension
    {
        public static IServiceCollection AddBaseWebApiServices(this IServiceCollection services)
        {
            services.AddGrpc();

            return services;
        }
    }
}
