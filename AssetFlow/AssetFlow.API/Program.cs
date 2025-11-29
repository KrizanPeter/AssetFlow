using AssetFlow.API.Endpoints;
using AssetFlow.API.Extensions;
using AssetFlow.Application.MediatR.Commands;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

IdentityExtensionDI.AddIdentityServices(builder.Services, builder.Configuration);
MapsterExtensionDI.AddMapsterServices(builder.Services);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    IEndpointRouteBuilder registeredRouteBuilder = AuthEndpoints.RegisterUserRoutes(endpoints);
});

app.Run();
