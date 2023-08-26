using AutoMapper;
using ITicket.uz.Application.Commons.Exceptions;
using ITicket.uz.Application.Commons.Interfaces;
using ITicket.uz.Application.Commons.Models;
using ITicket.uz.Domain.Entities;
using MediatR;
namespace ITicket.uz.Application.UseCases.Rows.Queries.GetAllRows;
public class GetAllRowsQuery:IRequest<PaginatedList<GetAllRowsQueryResponse>>
{
    public string? SearchingText { get; set; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
public class GetAllRowsQueryHandler:IRequestHandler<GetAllRowsQuery, PaginatedList<GetAllRowsQueryResponse>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public GetAllRowsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<PaginatedList<GetAllRowsQueryResponse>> Handle(GetAllRowsQuery request, CancellationToken cancellationToken)
    {
        var pageSize = request.PageSize;
        var pageNumber = request.PageNumber;
        var searchingText = request.SearchingText?.Trim();

        var rows = _context.Rows.AsQueryable();
        if (!string.IsNullOrEmpty(searchingText))
        {
            rows = rows.Where(p => p.Number.ToString().ToLower().Contains(searchingText.ToLower()));
        }
        if (rows is null || rows.Count() <= 0)
            throw new NotFoundException(nameof(Row), searchingText);
        var paginatedRows = await PaginatedList<Row>.CreateAsync(rows, pageNumber, pageSize);

        var clientResponses = _mapper.Map<List<GetAllRowsQueryResponse>>(paginatedRows.Items);

        var result = new PaginatedList<GetAllRowsQueryResponse>(clientResponses, paginatedRows.TotalCount, paginatedRows.PageNumber, paginatedRows.TotalPages);
        return result;
    }
}
public class GetAllRowsQueryResponse
{
    public Guid Id { get; set; }
    public Guid VenueId { get; set; }
    public int Number { get; set; }
    public int SeatsEachRow { get; set; }
}