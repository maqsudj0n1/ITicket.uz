using AutoMapper;
using ITicket.uz.Application.Commons.Exceptions;
using ITicket.uz.Application.Commons.Interfaces;
using ITicket.uz.Application.Commons.Models;
using ITicket.uz.Domain.Entities;
using MediatR;

namespace ITicket.uz.Application.UseCases.Seats.Queries;
public class GetAllSeatsQuery:IRequest<PaginatedList<GetAllSeatsQueryResponse>>
{
    public string? SearchingText { get; set; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
public class GetAllSeatsQueryHandler:IRequestHandler<GetAllSeatsQuery, PaginatedList<GetAllSeatsQueryResponse>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public GetAllSeatsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<PaginatedList<GetAllSeatsQueryResponse>> Handle(GetAllSeatsQuery request, CancellationToken cancellationToken)
    {
        var pageSize = request.PageSize;
        var pageNumber = request.PageNumber;
        var searchingText = request.SearchingText?.Trim();

        var seats = _context.Seats.AsQueryable();
        if (!string.IsNullOrEmpty(searchingText))
        {
            seats = seats.Where(p => p.Number.ToString().ToLower().Contains(searchingText.ToLower()));
        }
        if (seats is null || seats.Count() <= 0)
            throw new NotFoundException(nameof(Seat), searchingText);
        var paginatedSeats = await PaginatedList<Seat>.CreateAsync(seats, pageNumber, pageSize);

        var seatResponses = _mapper.Map<List<GetAllSeatsQueryResponse>>(paginatedSeats.Items);

        var result = new PaginatedList<GetAllSeatsQueryResponse>(seatResponses, paginatedSeats.TotalCount, paginatedSeats.PageNumber, paginatedSeats.TotalPages);
        return result;
    }
}
public class GetAllSeatsQueryResponse
{
    public Guid Id { get; set; }
    public Guid RowId { get; set; }
    public int Number { get; set; }
}