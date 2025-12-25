using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AssetFlow.Persistence.Contexts;
using AssetFlow.Domain.Entities.Auth;
using Duende.IdentityServer;

namespace AssetFlow.API.Extensions
{
    public static class IdentityExtensionDI
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("IdentityConnection"),
                    npgsqlOptions =>
                    {
                        npgsqlOptions.MigrationsAssembly("AssetFlow.Persistence");
                        npgsqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", "IdentitySchema");
                    }));

            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddAspNetIdentity<AppUser>();

            return services;
        }
    }
}