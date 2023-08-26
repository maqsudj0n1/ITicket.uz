using ITicket.uz.Application.Commons.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITicket.uz.Application.UseCases.Reservations.Commands.ReturnTicketCustomer
{
    public class ReturnTicketCustomerCommand:IRequest<bool>
    {
        public Guid ReservationId { get; set; }
    }
    public class ReturnTicketCustomerCommandHandler : IRequestHandler<ReturnTicketCustomerCommand,bool>
    {
        private readonly IApplicationDbContext _context;
        public ReturnTicketCustomerCommandHandler(IApplicationDbContext context)
        {
            _context=context;
        }
        public async Task<bool> Handle(ReturnTicketCustomerCommand request, CancellationToken cancellationToken)
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
            var res = _context.ScheduledEvents.Include(a => a.Reservations).Where(s=> s.Reservations.Any(x => x.Id == request.ReservationId)).FirstOrDefault();
            if((res.Start - DateTime.Now).TotalHours >= 24)
            {
                reservation.IsCancelled = true;

                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }

           return false;
        }
    }
}