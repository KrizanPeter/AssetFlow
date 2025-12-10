namespace AssetFlow.Application.Dtos.Auth
{
    public record CreateUserDto
    {
        public string UserName { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
    }
}
