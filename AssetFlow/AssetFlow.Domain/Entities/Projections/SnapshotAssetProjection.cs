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

    // Update the view when a snapshot is added
    public void Apply(SnapshotCreated e, SnapshotAssetView view)
    {
        if (view.Snapshots == null)
        {
            view.Snapshots = new List<SnapshotView>();
        }

        var snapshot = new SnapshotView
        {
            Id = e.Id,
            Balance = e.Balance,
            CreatedAt = e.CreatedAt
        };

        view.Snapshots.Add(snapshot);
        view.Balance = e.Balance;
        view.DateOfLastModification = e.CreatedAt;
    }
}
