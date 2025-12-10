using Mapster;
using MapsterMapper;

namespace AssetFlow.API.Extensions
{
    public static class MapsterExtensionDI
    {
        public static IServiceCollection AddMapsterServices(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(typeof(Program).Assembly);

            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();

            
            return services;
        }
    }
}