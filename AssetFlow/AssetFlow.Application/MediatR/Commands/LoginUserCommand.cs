using AssetFlow.Application.Dtos;
using FluentResults;
using MediatR;


namespace AssetFlow.Application.MediatR.Commands
{
    public record LoginUserCommand(string Email,  string Password) : IRequest<Result<UserDto>>
    {
        public static LoginUserCommand Of(LoginUserDto dto) =>
            new(dto.Email, dto.Password);
    }
}
