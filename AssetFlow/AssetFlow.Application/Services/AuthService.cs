using AssetFlow.Application.Dtos;
using AssetFlow.Application.Interfaces.IRepositories;
using AssetFlow.Application.Interfaces.IServices;
using AssetFlow.Domain.Entities.Auth;
using FluentResults;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssetFlow.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly ILogger<AuthService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        //private readonly IAccountService _accountService;
        public AuthService(ILogger<AuthService> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            IUserRepository userRepository, ITokenService tokenService, /*IAccountService accountService*/, IMapper mapper)
        {
            //_accountService = accountService;
            _logger = logger;
            _tokenService = tokenService;
            _userRepository = userRepository;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Result<UserDto>> RegisterNewUserAsync(CreateUserDto registerDto)
        {
            var userEntity = _mapper.Map<AppUser>(registerDto);
            userEntity.DateOfCreation = DateTime.Now;
            userEntity.DateOfLastModification = DateTime.Now;

            var result = await _userManager.CreateAsync(userEntity, registerDto.Password);

            if (!result.Succeeded)
            {
                var msg = string.Join(String.Empty, result.Errors.Select(a => a.Description).ToList());
                return Result.Fail<UserDto>(msg);
            }

            //var accountResult = await _accountService.CreateNewAccountForUser(userEntity.Id);

            //if (accountResult.IsFailure)
            //{
            //    return Result.Fail<UserDto>(accountResult.Error);
            //}

            //var updatedUserEntity = _userRepository.UpdateAccountId(userEntity.Id, accountResult.Value.AccountId);

            //if (updatedUserEntity.IsSuccess)
            //{
            //    var registeredUser = _mapper.Map<UserDto>(updatedUserEntity.Value);
            //    registeredUser.Token = _tokenService.GetToken(userEntity);
            //    return await CheckPassAndLogIn(registerDto);
            //}
            //return Result.Fail<UserDto>(updatedUserEntity.Error);
            return Result.Ok(new UserDto());
        }

        public async Task<Result<UserDto>> CheckPassAndLogIn(AppUser user, string password)
        {
            var domainUser = _userManager.Users.FirstOrDefault(predicate: a => a.Email.Equals(user.Email, StringComparison.CurrentCultureIgnoreCase));
            
            if (domainUser == null) 
                return Result.Fail<UserDto>("User does not exist.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (!result.Succeeded)
            {
                return Result.Fail<UserDto>("Wrong password.");
            }

            var loggedUser = _mapper.Map<UserDto>(domainUser);
            loggedUser.Token = _tokenService.GetToken(domainUser);

            return Result.Ok(loggedUser);
        }
    }
}
