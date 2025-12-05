
namespace AssetFlow.Domain.Entities
{
    public class Account : IEntity
    {
        public Guid Id { get; set; }
        public Guid AppUserId { get; set; }
        public AccountSettings AccountSettings { get; set; } = new AccountSettings();
        public DateTime DateOfCreation { get; set; }
        public DateTime DateOfLastModification { get; set; }

    }
}
