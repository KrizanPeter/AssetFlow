using AssetFlow.Application.Dtos.Auth;
using AssetFlow.Application.Errors;
using AssetFlow.Application.Interfaces.IRepositories;
using AssetFlow.Application.Interfaces.IServices;
using AssetFlow.Domain.Entities.Auth;
using FluentResults;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AssetFlow.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly ILogger<AuthService> _logger;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthService(ILogger<AuthService> logger,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService,
            IMapper mapper)
        {
            _logger = logger;
            _tokenService = tokenService;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Result<UserDto>> RegisterNewUserAsync(AppUser user, string password)
        {
            user.AccountId = Guid.NewGuid();
            user.DateOfCreation = DateTime.UtcNow;
            user.DateOfLastModification = DateTime.UtcNow;

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                var reasons = result.Errors.Select(a => new Error(a.Description)).ToList();
                return Result.Fail<UserDto>(ValidationError.Of("UserValidation", "Validation failed", reasons));
            }

            // After successful creation, automatically attempt to log the user in and return the logged in DTO (with token)
            return await CheckPassAndLogIn(user, password);
        }

        public async Task<Result<UserDto>> CheckPassAndLogIn(AppUser user, string password)
        {
            if (user == null || string.IsNullOrWhiteSpace(user.Email))
                return Result.Fail<UserDto>("Invalid credentials.");

            // Find the persisted user record (async, uses identity store)
            var domainUser = await _userManager.FindByEmailAsync(user.Email);
            if (domainUser == null)
                return Result.Fail<UserDto>("User does not exist.");

            // Validate password against the persisted user
            var signInResult = await _signInManager.CheckPasswordSignInAsync(domainUser, password, lockoutOnFailure: false);
            if (!signInResult.Succeeded)
                return Result.Fail<UserDto>("Wrong password.");

            var loggedUser = _mapper.Map<UserDto>(domainUser);
            loggedUser.Token = _tokenService.GetToken(domainUser);

            return Result.Ok(loggedUser);
        }
    }
}
