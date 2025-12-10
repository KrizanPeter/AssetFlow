
namespace AssetFlow.Shared.Contexts
{
    public class UserContext : IUserContext
    {
        public Guid UserId { get; set; } = Guid.Empty;
        public Guid AccountId { get; set; } = Guid.Empty;
    }
}
