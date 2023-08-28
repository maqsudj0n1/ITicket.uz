using AutoMapper;
using ITicket.uz.Application.Commons.Exceptions;
using ITicket.uz.Application.Commons.Interfaces;
using ITicket.uz.Application.Commons.Models;
using ITicket.uz.Domain.Entities;
using MediatR;
namespace ITicket.uz.Application.UseCases.ScheduledEvents.Queries.GetAllScheduledEvent;
public class GetAllScheduledEventQuery:IRequest<PaginatedList<GetAllScheduledEventQueryResponse>>
{
    public string? SearchingText { get; set; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
public class GetAllScheduledEventQueryHandler:IRequestHandler<GetAllScheduledEventQuery, PaginatedList<GetAllScheduledEventQueryResponse>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public GetAllScheduledEventQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<PaginatedList<GetAllScheduledEventQueryResponse>> Handle(GetAllScheduledEventQuery request, CancellationToken cancellationToken)
    {
        var pageSize = request.PageSize;
        var pageNumber = request.PageNumber;
        var searchingText = request.SearchingText?.Trim();

        var scheduledEvents = _context.ScheduledEvents.AsQueryable();
        if (!string.IsNullOrEmpty(searchingText))
        {
            scheduledEvents = scheduledEvents.Where(p => p.Price.ToString().ToLower().Contains(searchingText.ToLower()));
        }
        if (scheduledEvents is null || scheduledEvents.Count() <= 0)
            throw new NotFoundException(nameof(ScheduledEvent), searchingText);
        var paginatedScheduledEvents = await PaginatedList<ScheduledEvent>.CreateAsync(scheduledEvents, pageNumber, pageSize);

        var scheduledResponses = _mapper.Map<List<GetAllScheduledEventQueryResponse>>(paginatedScheduledEvents.Items);

        var result = new PaginatedList<GetAllScheduledEventQueryResponse>(scheduledResponses, paginatedScheduledEvents.TotalCount, request.PageNumber, request.PageSize);
        return result;

    }
}
public class GetAllScheduledEventQueryResponse
{
    public Guid Id { get; set; }
    public Guid EventId { get; set; }
    public Guid VenueId { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public decimal Price { get; set; }
}