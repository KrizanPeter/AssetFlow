

namespace AssetFlow.Domain.Events
{
    public record SnapshotAssetCreated(Guid AssetId, Guid AccountId, string AssetName, string UnitType, DateTime DateOfCreation, DateTime DateOfLastModification);
    public record LedgerAssetCreated(Guid AssetId, Guid AccountId, string AssetName, string UnitType, DateTime DateOfCreation, DateTime DateOfLastModification);

}
