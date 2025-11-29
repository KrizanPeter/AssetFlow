using AssetFlow.Application.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssetFlow.Application.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<Result<UserDto>> RegisterNewUserAsync(CreateUserDto registerDto);
        Task<Result<UserDto>> CheckPassAndLogIn(CreateUserDto loginUser);
    }
}
