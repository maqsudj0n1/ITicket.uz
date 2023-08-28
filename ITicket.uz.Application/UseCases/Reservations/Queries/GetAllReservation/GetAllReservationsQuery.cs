using AutoMapper;
using ITicket.uz.Application.Commons.Exceptions;
using ITicket.uz.Application.Commons.Interfaces;
using ITicket.uz.Application.Commons.Models;
using ITicket.uz.Domain.Entities;
using MediatR;
namespace ITicket.uz.Application.UseCases.Reservations.Queries.GetAllReservation;
public class GetAllReservationsQuery:IRequest<PaginatedList<GetAllReservationsQueryResponse>>
{
    public string? SearchingText { get; set; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
public class GetAllReservationsQueryHandler : IRequestHandler<GetAllReservationsQuery, PaginatedList<GetAllReservationsQueryResponse>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public GetAllReservationsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<PaginatedList<GetAllReservationsQueryResponse>> Handle(GetAllReservationsQuery request, CancellationToken cancellationToken)
    {
        var pageSize = request.PageSize;
        var pageNumber = request.PageNumber;
        var searchingText = request.SearchingText?.Trim();

        var reservations = _context.Reservations.AsQueryable();
        if (!string.IsNullOrEmpty(searchingText))
        {
            reservations = reservations.Where(p => p.ScheduledEventId.ToString().ToLower().Contains(searchingText.ToLower()));
        }
        if (reservations is null || reservations.Count() <= 0)
            throw new NotFoundException(nameof(Reservation), searchingText);
        var paginatedReservations = await PaginatedList<Reservation>.CreateAsync(reservations, pageNumber, pageSize);

        var reservationResponses = _mapper.Map<List<GetAllReservationsQueryResponse>>(paginatedReservations.Items);

        var result = new PaginatedList<GetAllReservationsQueryResponse>(reservationResponses, paginatedReservations.TotalCount, request.PageNumber, request.PageSize);
        return result;

    }
}
public class GetAllReservationsQueryResponse
{
    public Guid Id { get; set; }
    public Guid SeatId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid ScheduledEventId { get; set; }
    public bool IsCancelled { get; set; }
}