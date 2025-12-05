using AssetFlow.Domain.Entities.Auth;

namespace AssetFlow.Application.Interfaces.IRepositories.Auth
{
    public interface IUserRepository : IRepository<AppUser>
    {
        void Update(AppUser user);
        void SaveChanges();
    }
}
