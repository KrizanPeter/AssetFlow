using Microsoft.AspNetCore.Identity;


namespace AssetFlow.Domain.Entities.Auth
{
    public class AppUserRole : IdentityUserRole<Guid>
    {
        public virtual required AppUser AppUser { get; set; }
        public virtual required AppRole AppRole { get; set; }
    }
}
