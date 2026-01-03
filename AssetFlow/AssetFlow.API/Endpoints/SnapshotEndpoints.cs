
using AssetFlow.API.Extensions;
using AssetFlow.Application.Dtos.Asset;
using AssetFlow.Application.MediatR.Commands;
using AssetFlow.Application.MediatR.Queries;
using AssetFlow.Shared.Contexts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AssetFlow.API.Endpoints
{
    public class SnapshotEndpoints
    {
        private const string SWAGGER_TAG = "Snapshot";
        public static IEndpointRouteBuilder RegisterSnapshotRoutes(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/api/snapshot/{id}", AddSnapshot).WithTags(SWAGGER_TAG);



            return endpoints;
        }

        private static async Task<IResult> AddSnapshot(HttpContext context, AddSnapshotDto dto,
            IMediator mediator, ILogger<SnapshotEndpoints> logger)
        {
            try
            {
                var assets = await mediator.Send(AddSnapshotCommand.Of(dto));
                return assets.ToApiResult();
            }

            catch (Exception ex)
            {
                logger.LogError(ex, "Error registering user");

                return Results.Problem(
                    detail: "An unexpected error occurred while getting the asset.",
                    statusCode: StatusCodes.Status500InternalServerError,
                    title: "Internal Server Error"
                );
            }
        }
    }
}
