using AssetFlow.Application.Dtos.Auth;
using AssetFlow.Application.MediatR.Commands;
using AssetFlow.Domain.Entities.Auth;
using AssetFlow.Domain.Events;
using Mapster;

namespace AssetFlow.Application.MapsterConfig
{
    internal class MapsterConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AppUser, UserDto>();
            config.NewConfig<AppUser, CreateUserDto>();
            config.NewConfig<AppUser, LoginUserDto>();

            config.NewConfig<UserDto, AppUser>();
            config.NewConfig<CreateUserDto, AppUser>();
            config.NewConfig<LoginUserDto, AppUser>();

            config.NewConfig<CreateUserCommand, AppUser>();

            //Events mapping
            config.NewConfig<CreateAssetCommand, SnapshotAssetCreated>();
            config.NewConfig<CreateAssetCommand, LedgerAssetCreated>();

        }
    }
}
