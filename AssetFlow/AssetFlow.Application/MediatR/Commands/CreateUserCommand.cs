using AssetFlow.Application.Dtos.Auth;
using FluentResults;
using MediatR;


namespace AssetFlow.Application.MediatR.Commands;

public record CreateUserCommand(string UserName, string Email,  string Password) : IRequest<Result<UserDto>>
{
    public static CreateUserCommand Of(CreateUserDto dto) =>
        new(dto.UserName, dto.Email, dto.Password);
}


