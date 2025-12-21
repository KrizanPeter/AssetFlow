using AssetFlow.API.Extensions;
using AssetFlow.Application.Dtos.Asset;
using AssetFlow.Application.MediatR.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AssetFlow.API.Endpoints
{
    public class AssetEndpoints
    {
        private const string SWAGGER_TAG = "Asset";
        public static IEndpointRouteBuilder RegisterAssetRoutes(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/api/asset/create", CreateAsset).WithTags(SWAGGER_TAG);
            endpoints.MapGet("/api/asset/{id}", GetAsset).WithTags(SWAGGER_TAG);
            endpoints.MapGet("/api/asset/all", GetAllAssets).WithTags(SWAGGER_TAG);


            return endpoints;
        }

        private static async Task GetAllAssets(HttpContext context,
            IMediator mediator,
            ILogger<AuthEndpoints> logger,
            CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        private static async Task GetAsset(
            HttpContext context,
            [FromRoute] Guid id,
            IMediator mediator,
            ILogger<AuthEndpoints> logger,
            CancellationToken ct)
        {
            try
            {
                //var user = await mediator.Send(GetAssetQuery.Of(createAssetDto), ct);
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

        private static async Task<IResult> CreateAsset(
            HttpContext context,
            [FromBody] CreateAssetDto createAssetDto,
            IMediator mediator,
            ILogger<AuthEndpoints> logger,
            CancellationToken ct)
        {
            try
            {
                var user = await mediator.Send(CreateAssetCommand.Of(createAssetDto), ct);
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
    }
}
