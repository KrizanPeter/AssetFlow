using AssetFlow.Application.Dtos;
using MediatR;


namespace AssetFlow.Application.MediatR.Commands
{
    public class LoginUserCommand(string Email,  string Password) : IRequest<UserDto>
    {
        public static LoginUserCommand Of(LoginUserDto dto) =>
            new(dto.Email, dto.Password);
    }
}
