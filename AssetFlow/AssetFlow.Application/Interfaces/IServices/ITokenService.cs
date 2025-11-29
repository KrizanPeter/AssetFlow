using AssetFlow.Domain.Entities.Auth;

namespace AssetFlow.Application.Interfaces.IServices
{
    public interface ITokenService
    {
        string GetToken(AppUser user);
    }
}
