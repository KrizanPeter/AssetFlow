using AssetFlow.Application.Dtos.Auth;
using AssetFlow.Application.Interfaces.IServices;
using AssetFlow.Domain.Entities.Auth;
using FluentResults;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AssetFlow.Application.MediatR.Commands.Handlers
{
    public sealed class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<UserDto>>
    {
        private readonly IAuthService _authService;
        private readonly ILogger<CreateUserCommandHandler> _logger;
        private readonly IMapper _mapper;

        public LoginUserCommandHandler(ILogger<CreateUserCommandHandler> logger, IAuthService authService, IMapper mapper)
        {
            _logger = logger;
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<Result<UserDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = _mapper.Map<AppUser>(request);
                var loggedUser = await _authService.CheckPassAndLogIn(user, request.Password);

                return loggedUser;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user");
                throw;
            }
        }
    }
}
