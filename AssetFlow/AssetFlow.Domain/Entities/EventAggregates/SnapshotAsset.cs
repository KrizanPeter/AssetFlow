
using AssetFlow.Domain.Events;

namespace AssetFlow.Domain.Entities.EventAggregates
{
    public class SnapshotAsset : Asset
    {
        public List<Snapshot> Snapshots { get; set; } = new();

        public SnapshotAsset() { }

        public void Apply(SnapshotAssetCreated e)
        {
            Id = e.AssetId; 
            AccountId = e.AccountId;
            AssetName = e.AssetName;
            UnitType = e.UnitType;
            Balance = Decimal.Zero;

            DateOfCreation = e.DateOfCreation;
            DateOfLastModification = e.DateOfLastModification;
        }
    }
}
