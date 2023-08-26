using ITicket.uz.Domain.Commons;
using System.Text.Json.Serialization;

namespace ITicket.uz.Domain.Identity
{
    public class Role:BaseAuditableEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }
        [JsonIgnore]
        public virtual ICollection<User>? Users { get; set; }
    }
}