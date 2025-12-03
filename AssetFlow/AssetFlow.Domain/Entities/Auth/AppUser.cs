using Microsoft.AspNetCore.Identity;

namespace AssetFlow.Domain.Entities.Auth
{
    public class AppUser : IdentityUser<Guid>, IEntity
    {
        public Guid? AccountId { get; set; }
        public virtual ICollection<AppUserRole>? AppUserRoles { get; set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime DateOfLastModification { get; set; }
    }
}
