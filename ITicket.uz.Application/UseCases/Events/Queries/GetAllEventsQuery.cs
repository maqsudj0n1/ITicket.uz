using AutoMapper;
using ITicket.uz.Application.Commons.Exceptions;
using ITicket.uz.Application.Commons.Interfaces;
using ITicket.uz.Application.Commons.Models;
using ITicket.uz.Domain.Entities;
using MediatR;
namespace ITicket.uz.Application.UseCases.Events.Queries;
public class GetAllEventsQuery:IRequest<PaginatedList<GetAllEventsQueryResponse>>
{
    public string? SearchingText { get; set; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;

}
public class GetAllEventQueryHandler:IRequestHandler<GetAllEventsQuery, PaginatedList<GetAllEventsQueryResponse>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public GetAllEventQueryHandler(IMapper mapper, IApplicationDbContext context)
    {
        _mapper=mapper;
        _context=context;
    }
    public async Task<PaginatedList<GetAllEventsQueryResponse>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
    {
        var pageSize = request.PageSize;
        var pageNumber = request.PageNumber;
        var searchingText = request.SearchingText?.Trim();

        var events = _context.Events.AsQueryable();
        if (!string.IsNullOrEmpty(searchingText))
        {
            events = events.Where(x=> x.Description.ToLower().Contains(searchingText.ToLower()));
        }
        if (events is null || events.Count() <=0)
            throw new NotFoundException(nameof(Event), searchingText);
        var paginatedEvents = await PaginatedList<Event>.CreateAsync(events, pageNumber, pageSize);
        var evetResponses = _mapper.Map<List<GetAllEventsQueryResponse>>(paginatedEvents.Items);
        var result = new PaginatedList<GetAllEventsQueryResponse>(evetResponses, paginatedEvents.TotalCount, paginatedEvents.PageNumber, paginatedEvents.TotalPages);
        return result;
    }
}
public class GetAllEventsQueryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}