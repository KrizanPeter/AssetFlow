using Microsoft.AspNetCore.Identity;


namespace AssetFlow.Domain.Entities.Auth
{
    public class AppRole : IdentityRole<Guid>
    {
        public ICollection<AppUserRole> AppUserRoles { get; set; } = new List<AppUserRole>();

    }
}
