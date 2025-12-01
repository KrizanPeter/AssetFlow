using Microsoft.AspNetCore.Identity;


namespace AssetFlow.Domain.Entities.Auth
{
    public class AppRole : IdentityRole<Guid>, IEntity
    {
        public ICollection<AppUserRole> AppUserRoles { get; set; } = new List<AppUserRole>();
        public DateTime DateOfCreation { get; set; }
        public DateTime DateOfLastModification { get; set; }
    }
}
