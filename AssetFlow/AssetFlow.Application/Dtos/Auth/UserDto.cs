namespace AssetFlow.Application.Dtos.Auth
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
