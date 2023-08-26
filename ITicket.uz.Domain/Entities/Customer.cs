using ITicket.uz.Domain.Commons;
namespace ITicket.uz.Domain.Entities;
public class Customer:BaseAuditableEntity
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public virtual ICollection<Reservation> Reservations { get; set; }
}