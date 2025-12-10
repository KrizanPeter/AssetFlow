
namespace AssetFlow.Shared.Contexts
{
    public class UserContext : IUserContext
    {
        public string UserId { get; set; } = string.Empty;
        public string AccountId { get; set; } = string.Empty;
    }
}
