using ITicket.uz.Domain.Commons;
using System.Text.Json.Serialization;
namespace ITicket.uz.Domain.Identity;
public class Permission:BaseAuditableEntity
{
    public string Name { get; set; }
    [JsonIgnore]
    public virtual ICollection<Role>? Roles { get; set; }
}