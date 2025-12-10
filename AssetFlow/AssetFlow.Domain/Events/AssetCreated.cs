

using AssetFlow.Domain.Enums;

namespace AssetFlow.Domain.Events
{
    public record AssetCreated(Guid AssetId, Guid AccountId, string AssetName, AssetType TypeOfAsset, string UnitType);
}
