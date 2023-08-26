using ITicket.uz.Domain.Commons;
namespace ITicket.uz.Domain.Entities;
public class Seat:BaseAuditableEntity
{
    public Guid RowId { get; set; }
    public int Number { get; set; }
    public virtual Row Row { get; set;}
    public virtual ICollection<Reservation> Reservations { get; set; }
}