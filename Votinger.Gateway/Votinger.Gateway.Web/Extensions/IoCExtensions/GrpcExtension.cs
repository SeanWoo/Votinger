using Grpc.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Votinger.Gateway.Web.Interceptors;
using Votinger.Protos;

namespace Votinger.Gateway.Web.Extensions.IoCExtensions
{
    public static class GrpcExtension
    {
        public static IServiceCollection AddGrpcFactories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<JwtInterceptor>();

            services.AddGrpcClient<Test.TestClient>(x =>
            {
                x.Address = new Uri(configuration["GrpcEndpoints:PollServer"]);
                var httpHandler = new HttpClientHandler();
                httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                x.ChannelOptionsActions.Add(y => y.HttpHandler = httpHandler);
            })
                .AddInterceptor<JwtInterceptor>()
                .ConfigureChannel(o =>
                {
                    var callCredentials = CallCredentials.FromInterceptor((context, metadata) =>
                    {
                        metadata.Add("Authorization", "asd");
                        return Task.CompletedTask;
                    });
                    o.Credentials = ChannelCredentials.Create(new SslCredentials(), callCredentials);
                });
            services.AddGrpcClient<GrpcPollService.GrpcPollServiceClient>(x =>
            {
                x.Address = new Uri(configuration["GrpcEndpoints:PollServer"]);
                var httpHandler = new HttpClientHandler();
                httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                x.ChannelOptionsActions.Add(y => y.HttpHandler = httpHandler);
            })
                .AddInterceptor<JwtInterceptor>(); ;
            services.AddGrpcClient<GrpcUserService.GrpcUserServiceClient>(x =>
            {
                x.Address = new Uri(configuration["GrpcEndpoints:AuthServer"]);
                var httpHandler = new HttpClientHandler();
                httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                x.ChannelOptionsActions.Add(y => y.HttpHandler = httpHandler);
            })
                .AddInterceptor<JwtInterceptor>(); ;

            return services;
        }
    }
}
