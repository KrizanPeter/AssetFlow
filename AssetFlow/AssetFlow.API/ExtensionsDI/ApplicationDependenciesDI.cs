using AssetFlow.Application.Interfaces.IRepositories;
using AssetFlow.Application.Interfaces.IServices;
using AssetFlow.Application.Services;
using AssetFlow.Persistence.Repositories;
using AssetFlow.Shared.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AssetFlow.API.ExtensionsDI
{
    public static class ApplicationDependenciesDI
    {
        public static IServiceCollection AddAppDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            //Auth related registrations
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();

            //Domain services registrations can go here
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IDocumentRepositoryDb<>), typeof(DocumentRepositoryDb<>)); 
            services.AddScoped<IUserContext, UserContext>();
            return services;
        }
    }
}
