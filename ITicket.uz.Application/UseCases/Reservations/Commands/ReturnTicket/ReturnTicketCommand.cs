using ITicket.uz.Application.Commons.Interfaces;
using MediatR;
namespace ITicket.uz.Application.UseCases.Reservations.Commands.ReturnTicket;

public class ReturnTicketCommand:IRequest<bool>
{
    public Guid ReservationId { get; set; }
}
public class ReturnTicketCommandHandler:IRequestHandler<ReturnTicketCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public ReturnTicketCommandHandler(IApplicationDbContext context)
    {
        _context=context;
    }
    public async Task<bool> Handle(ReturnTicketCommand request, CancellationToken cancellationToken)
    {
        var reservation = await _context.Reservations.FindAsync(request.ReservationId);

        if (reservation == null)
        {
            return false;
        }

        if (reservation.IsCancelled)
        {
            return false;
        }

        reservation.IsCancelled = true;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}