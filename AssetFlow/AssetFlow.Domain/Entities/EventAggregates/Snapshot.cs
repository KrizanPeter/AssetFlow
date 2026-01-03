
using AssetFlow.Domain.Events;

namespace AssetFlow.Domain.Entities.EventAggregates
{
    public class Snapshot
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Balance { get; set; } = decimal.Zero;

        public static Snapshot Of (SnapshotCreated e)
        {
            return new Snapshot()
            {
                Id = e.Id,
                CreatedAt = e.CreatedAt,
                Balance = e.Balance
            };
        }
    }
}
