using AutoMapper;
using ITicket.uz.Application.Commons.Interfaces;
using ITicket.uz.Domain.Entities;
using MediatR;
namespace ITicket.uz.Application.UseCases.Events.Commands.CreateEvent;

public class CreateEventCommand:IRequest<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; }
}
public class CreateEventCommandHandler:IRequestHandler<CreateEventCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateEventCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context=context;
        _mapper=mapper;
    }
    public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
            Event _event = _mapper.Map<Event>(request);
            await _context.Events.AddAsync(_event);
            await _context.SaveChangesAsync();
            return _event.Id; 
    }
}