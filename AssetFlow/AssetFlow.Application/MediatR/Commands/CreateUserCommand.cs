using AssetFlow.Application.Dtos;
using MediatR;


namespace AssetFlow.Application.MediatR.Commands;

public record CreateUserCommand(string Username, string Email,  string Password) : IRequest<UserDto>
{
    public static CreateUserCommand Of(CreateUserDto dto) =>
        new(dto.Username, dto.Email, dto.Password);
}


