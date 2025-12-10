using AssetFlow.Application.Dtos.Asset;
using FluentResults;
using MediatR;

namespace AssetFlow.Application.MediatR.Commands
{
    public record CreateAssetCommand(string AssetName, AssetType TypeOfAsset, string UnitType) : IRequest<Result<AssetDto>>
    {
        public static CreateAssetCommand Of(CreateAssetDto dto) =>
            new(dto.AssetName, dto.TypeOfAsset, dto.UnitType);
    }
}
