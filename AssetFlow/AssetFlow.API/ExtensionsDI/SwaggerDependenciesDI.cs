using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;

namespace AssetFlow.API.ExtensionsDI
{
    public static class SwaggerDependenciesDI
    {
        internal static void AddSwaggerServices(IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference("Bearer", document)] = []
                });
            });
        }
    }
}
