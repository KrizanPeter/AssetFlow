using AssetFlow.Domain.Enums;
using AssetFlow.Domain.Events;

namespace AssetFlow.Domain.Entities.EventAggregates
{
    public class Asset : IEntity
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public string AssetName { get; set; } = string.Empty;
        public decimal Balance { get; set; } = decimal.Zero;
        public string UnitType { get; set; } = string.Empty;
        public DateTime DateOfCreation { get; set; }
        public DateTime DateOfLastModification { get; set; }


        public Asset() { }

    }
}
