using AssetFlow.Application.Dtos;
using MediatR;

namespace AssetFlow.Application.MediatR.Commands.Handlers
{
    internal sealed class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, UserDto>
    {
        public Task<UserDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException("LoginUserCommandHandler is a placeholder. Implement authentication logic here.");
        }
    }
}
