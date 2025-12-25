using System;
using System.Collections.Generic;
using System.Text;

namespace AssetFlow.Domain.Entities.Projections
{
    public class SnapshotAssetView
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public string AssetName { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public string UnitType { get; set; } = string.Empty;

        public DateTime DateOfCreation { get; set; }
        public DateTime DateOfLastModification { get; set; }

        public List<SnapshotView> Snapshots { get; set; } = new();
    }

    public class SnapshotView
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Balance { get; set; } = decimal.Zero;
    }

}
