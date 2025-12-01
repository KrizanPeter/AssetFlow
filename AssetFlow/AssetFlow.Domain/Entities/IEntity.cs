
namespace AssetFlow.Domain.Entities
{
    internal interface IEntity
    {
        public Guid Id { get; set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime DateOfLastModification { get; set; }
    }
}
