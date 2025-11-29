using AssetFlow.Application.Dtos;
using AssetFlow.Domain.Entities.Auth;
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
        }
    }
}
