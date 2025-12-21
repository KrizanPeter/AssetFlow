
namespace AssetFlow.Domain.Entities.EventAggregates
{
    public class Snapshot
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Balance { get; set; } = decimal.Zero;
    }
}
