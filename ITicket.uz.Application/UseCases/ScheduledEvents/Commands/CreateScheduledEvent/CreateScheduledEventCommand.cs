using AutoMapper;
using ITicket.uz.Application.Commons.Exceptions;
using ITicket.uz.Application.Commons.Interfaces;
using ITicket.uz.Domain.Entities;
using MediatR;
namespace ITicket.uz.Application.UseCases.ScheduledEvents.Commands.CreateScheduledEvent;
public class CreateScheduledEventCommand : IRequest<Guid>
{
    public Guid EventId { get; set; }
    public Guid VenueId { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public decimal Price { get; set; }


}
public class CreateScheduledEventCommandHandler : IRequestHandler<CreateScheduledEventCommand, Guid>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateScheduledEventCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateScheduledEventCommand request, CancellationToken cancellationToken)
    {

        ScheduledEvent _event = _mapper.Map<ScheduledEvent>(request);
        bool isInterval = !_dbContext.ScheduledEvents.Any(e =>
           e.VenueId == request.VenueId &&
           !(request.End <= e.Start || request.Start >= e.End) &&
           (request.Start - e.End).TotalHours < 1 &&
           (e.Start - request.End).TotalHours < 1);
        if (isInterval)
        {
            await _dbContext.ScheduledEvents.AddAsync(_event, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return _event.Id;

        }
        throw new NotFoundException(nameof(ScheduledEvent), "Vaqt mos emas");

    }
}