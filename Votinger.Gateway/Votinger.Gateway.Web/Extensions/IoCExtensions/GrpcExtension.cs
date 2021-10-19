using Microsoft.Extensions.DependencyInjection;
using System;
using Votinger.Protos;

namespace Votinger.Gateway.Web.Extensions.IoCExtensions
{
    public static class GrpcExtension
    {
        public static IServiceCollection AddGrpcFactories(this IServiceCollection services)
        {
            services.AddGrpcClient<Test.TestClient>(x =>
            {
                x.Address = new Uri("https://localhost:5003");
            });
            services.AddGrpcClient<GrpcUser.GrpcUserClient>(x =>
            {
                x.Address = new Uri("https://localhost:5001");
            });

            return services;
        }
    }
}
