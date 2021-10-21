using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using Votinger.Protos;

namespace Votinger.Gateway.Web.Extensions.IoCExtensions
{
    public static class GrpcExtension
    {
        public static IServiceCollection AddGrpcFactories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGrpcClient<Test.TestClient>(x =>
            {
                x.Address = new Uri(configuration["GrpcEndpoints:PollServer"]);
                var httpHandler = new HttpClientHandler();
                httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                x.ChannelOptionsActions.Add(y => y.HttpHandler = httpHandler);
            });
            services.AddGrpcClient<GrpcUser.GrpcUserClient>(x =>
            {
                x.Address = new Uri(configuration["GrpcEndpoints:AuthServer"]);
                var httpHandler = new HttpClientHandler();
                httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                x.ChannelOptionsActions.Add(y => y.HttpHandler = httpHandler);
            });

            return services;
        }
    }
}
