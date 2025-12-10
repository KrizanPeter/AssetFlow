using System;
using System.Threading;
using System.Threading.Tasks;
using AssetFlow.Application.Dtos.Auth;
using AssetFlow.Application.Interfaces.IServices;
using AssetFlow.Domain.Entities.Auth;
using FluentResults;
using FluentResults.Extensions;
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
        private readonly IAccountService _accountService;

        public CreateUserCommandHandler(ILogger<CreateUserCommandHandler> logger, IAuthService authService, IMapper mapper, IAccountService accountService)
        {
            _logger = logger;
            _authService = authService;
            _mapper = mapper;
            _accountService = accountService;
        }

        public async Task<Result<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var appUser = _mapper.Map<AppUser>(request);
                UserDto? createdUser = null;

                var result = await _authService.RegisterNewUserAsync(appUser, request.Password)
                    .Map(user => createdUser = user)
                    .Bind(user => _accountService.CreateAccount(user.Id, user.AccountId));

                if (result.IsSuccess)
                    return Result.Ok(createdUser!);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user");
                throw;
            }
        }
    }
}
