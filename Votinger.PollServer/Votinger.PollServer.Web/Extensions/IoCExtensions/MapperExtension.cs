using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Votinger.PollServer.Services.Mapper;
using Votinger.PollServer.Web.Mapper;

namespace Votinger.PollServer.Web.Extensions.IoCExtensions
{
    public static class MapperExtension
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
                mc.AddProfile(new PollProfile());
            });

            services.AddSingleton(mappingConfig.CreateMapper());

            return services;
        }
    }
}
