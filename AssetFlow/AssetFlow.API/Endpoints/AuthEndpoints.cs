using AssetFlow.API.Extensions;
using AssetFlow.Application.Dtos.Auth;
using AssetFlow.Application.MediatR.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AssetFlow.API.Endpoints
{
    public class AuthEndpoints
    {
        private const string SWAGGER_TAG = "Authentication";
        public static IEndpointRouteBuilder RegisterUserRoutes (IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/api/user/register", RegisterUser).WithTags(SWAGGER_TAG);
            endpoints.MapPost("/api/user/login", LoginUser).WithTags(SWAGGER_TAG);
            endpoints.MapGet("/auth-test", (HttpContext ctx) =>
            {
                return new
                {
                    Authenticated = ctx.User.Identity?.IsAuthenticated,
                    Claims = ctx.User.Claims.Select(c => new { c.Type, c.Value })
                };
            }).RequireAuthorization();

            return endpoints;
        }

        private static async Task<IResult> RegisterUser(
            HttpContext context,
            [FromBody] CreateUserDto registerDto,
            IMediator mediator,
            ILogger<AuthEndpoints> logger,
            CancellationToken ct)
        {
            try
            {
                var user = await mediator.Send(CreateUserCommand.Of(registerDto), ct);
                return user.ToApiResult();
            }

            catch (Exception ex)
            {
                logger.LogError(ex, "Error registering user");

                return Results.Problem(
                    detail: "An unexpected error occurred while registering the user.",
                    statusCode: StatusCodes.Status500InternalServerError,
                    title: "Internal Server Error"
                );
            }
        }

        private static async Task<IResult> LoginUser(
            HttpContext context, 
            [FromBody] LoginUserDto registerDto, 
            IMediator mediator, 
            ILogger<AuthEndpoints> logger,  
            CancellationToken ct)
        {
            try
            {
                var result = await mediator.Send(LoginUserCommand.Of(registerDto), ct);

                return result.ToApiResult();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error logging in user");

                return Results.Problem(
                    detail: "An unexpected error occurred while logging in the user.",
                    statusCode: StatusCodes.Status500InternalServerError,
                    title: "Internal Server Error"
                );
            }
        }
    }
}
