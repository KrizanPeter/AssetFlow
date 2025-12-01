using System;
using System.Threading;
using System.Threading.Tasks;
using AssetFlow.Application.Dtos;
using AssetFlow.Application.Interfaces.IServices;
using AssetFlow.Domain.Entities.Auth;
using FluentResults;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AssetFlow.Application.MediatR.Commands.Handlers
{
    public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<UserDto>>
    {

        private readonly IAuthService _authService;
        private readonly ILogger<CreateUserCommandHandler> _logger;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(ILogger<CreateUserCommandHandler> logger, IAuthService authService, IMapper mapper)
        {
            _logger = logger;
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<Result<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = _mapper.Map<AppUser>(request);
                var createdUser = await _authService.RegisterNewUserAsync(user, request.Password);

                return createdUser;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user");
                throw;
            }
        }
    }
}
