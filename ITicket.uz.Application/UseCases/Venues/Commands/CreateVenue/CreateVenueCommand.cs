using AutoMapper;
using ITicket.uz.Application.Commons.Interfaces;
using ITicket.uz.Domain.Entities;
using MediatR;
namespace ITicket.uz.Application.UseCases.Venues.Commands.CreateVenue;
public class CreateVenueCommand:IRequest<Guid>
{
    public string Name { get; set; }
    public string Address { get; set; }
    public int Capacity { get; set; }

}
public class CreateVenueCommandHandler:IRequestHandler<CreateVenueCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateVenueCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context=context;
        _mapper=mapper;
    }
    public async Task<Guid> Handle(CreateVenueCommand request, CancellationToken cancellationToken)
    {
        Venue venue = _mapper.Map<Venue>(request);
        await _context.Venues.AddAsync(venue);
        await _context.SaveChangesAsync(cancellationToken);
        return venue.Id;
    }
}