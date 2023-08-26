using AutoMapper;
using ITicket.uz.Application.Commons.Interfaces;
using ITicket.uz.Domain.Entities;
using MediatR;
namespace ITicket.uz.Application.UseCases.Reservations.Commands.CreateReservation;
public class CreateReservationCommand:IRequest<Guid>
{
    public Guid SeatId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid ScheduledEventId { get; set; }
    public bool IsCancelled { get; set; } = false;
}
public class CreateReservationCommandHandler:IRequestHandler<CreateReservationCommand, Guid>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateReservationCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        Reservation reservation = _mapper.Map<Reservation>(request);

        await _dbContext.Reservations.AddAsync(reservation, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return reservation.Id;
    }
}