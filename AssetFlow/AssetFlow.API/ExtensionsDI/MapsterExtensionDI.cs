using System;
using System.Reflection;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace AssetFlow.API.Extensions
{
    public static class MapsterExtensionDI
    {
        public static void AddMapsterServices(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(typeof(Program).Assembly); 

            builder.Services.AddSingleton(config);
            builder.Services.AddScoped<IMapper, ServiceMapper>();
            return services;
        }
    }
}