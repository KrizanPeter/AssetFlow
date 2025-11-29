using AssetFlow.Domain.Entities.Auth;
using System.Text.Json.Serialization;

namespace AssetFlow.Domain.Entities
{
    public class Account
    {
        public Guid AccountId { get; set; }
        public Guid AppUserId { get; set; }

        [JsonIgnore]
        public virtual required AppUser AppUser { get; set; }
    }
}
