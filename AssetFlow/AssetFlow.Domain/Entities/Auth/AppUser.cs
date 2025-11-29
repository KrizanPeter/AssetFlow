using Microsoft.AspNetCore.Identity;

namespace AssetFlow.Domain.Entities.Auth
{
    public class AppUser : IdentityUser<Guid>
    {
        public Guid? AccountId { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid LastModifyBy { get; set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime DateOfLastModification { get; set; }
        public virtual Account? Account { get; set; }
        public virtual ICollection<AppUserRole>? AppUserRoles { get; set; }

    }
}
