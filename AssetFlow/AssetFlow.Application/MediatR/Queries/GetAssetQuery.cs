using AssetFlow.Application.Dtos.Asset;
using FluentResults;
using MediatR;

namespace AssetFlow.Application.MediatR.Queries
{
    public class GetAssetQuery : IRequest<Result<AssetDto>>
    {
        public Guid AssetId { get; set; }

        private GetAssetQuery(Guid assetId)
        {
            this.AssetId = assetId;
        }

        public static GetAssetQuery Of(Guid assetId)
        {
            return new GetAssetQuery(assetId);
        }
    }
}
