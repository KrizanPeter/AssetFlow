using Marten.Events.Aggregation;
using AssetFlow.Domain.Entities.Projections;
using AssetFlow.Domain.Events;

public class SnapshotAssetProjection : SingleStreamProjection<SnapshotAssetView, Guid>
{
    // Build the view from the creation event
    public SnapshotAssetView Create(SnapshotAssetCreated e)
    {
        return new SnapshotAssetView
        {
            Id = e.AssetId, 
            AccountId = e.AccountId,
            AssetName = e.AssetName,
            UnitType = e.UnitType,
            Balance = 0m,
            DateOfCreation = e.DateOfCreation,
            DateOfLastModification = e.DateOfLastModification,
            Snapshots = new List<SnapshotView>()
        };
    }
}
