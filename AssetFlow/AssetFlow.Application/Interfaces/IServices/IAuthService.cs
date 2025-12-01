using AssetFlow.Application.Dtos;
using AssetFlow.Application.MediatR.Commands;
using AssetFlow.Domain.Entities.Auth;
using FluentResults;

namespace AssetFlow.Application.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<Result<UserDto>> RegisterNewUserAsync(AppUser user, string password);
        Task<Result<UserDto>> CheckPassAndLogIn(AppUser user, string password);
    }
}
