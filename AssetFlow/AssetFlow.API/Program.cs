using AssetFlow.API.Endpoints;
using AssetFlow.API.Extensions;
using AssetFlow.API.ExtensionsDI;
using AssetFlow.Application.MediatR.Commands;
using AssetFlow.Persistence.ExtensionsDI;
using AssetFlow.Shared.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

IdentityExtensionDI.AddIdentityServices(builder.Services, builder.Configuration);
MapsterExtensionDI.AddMapsterServices(builder.Services);
ApplicationDependenciesDI.AddAppDependencies(builder.Services, builder.Configuration);
SwaggerDependenciesDI.AddSwaggerServices(builder.Services, builder.Configuration);
MartenDI.AddMartenServices(builder.Services, builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly));

var app = builder.Build();

app.UseMiddleware<UserContextMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; // Swagger at root
    });
}


app.UseHttpsRedirection();

app.UseRouting();
app.UseIdentityServer();

app.UseAuthorization();
app.UseAuthentication();
app.UseEndpoints(endpoints =>
{
    IEndpointRouteBuilder registeredRouteBuilder = AuthEndpoints.RegisterUserRoutes(endpoints);
});

app.Run();
