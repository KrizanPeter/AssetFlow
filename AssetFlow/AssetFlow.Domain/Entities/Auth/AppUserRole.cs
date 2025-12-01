using Microsoft.AspNetCore.Identity;


namespace AssetFlow.Domain.Entities.Auth
{
    public class AppUserRole : IdentityUserRole<Guid>, IEntity
    {
        public Guid Id { get; set; }
        public virtual required AppUser AppUser { get; set; }
        public virtual required AppRole AppRole { get; set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime DateOfLastModification { get; set; }
    }
}
