using AutoMapper;
using ITicket.uz.Application.Commons.Exceptions;
using ITicket.uz.Application.Commons.Interfaces;
using ITicket.uz.Application.Commons.Models;
using ITicket.uz.Domain.Entities;
using MediatR;
namespace ITicket.uz.Application.UseCases.Venues.Queries
{
    public class GetAllVenuesQuery:IRequest<PaginatedList<GetAllVenueQueryResponse>>
    {
        public string? SearchingText { get; set; }
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;

        public class GetAllVenuesQueryHandler:IRequestHandler<GetAllVenuesQuery, PaginatedList<GetAllVenueQueryResponse>>
        {
            private readonly IMapper _mapper;
            private readonly IApplicationDbContext _context;

            public GetAllVenuesQueryHandler(IMapper mapper, IApplicationDbContext context)
            {
                _mapper=mapper;
                _context=context;
            }
            public async Task<PaginatedList<GetAllVenueQueryResponse>> Handle(GetAllVenuesQuery request, CancellationToken cancellationToken)
            {
                var pageSize = request.PageSize;
                var pageNumber = request.PageNumber;
                var searchingText = request.SearchingText?.Trim();
                var Venues = _context.Venues.AsQueryable();
                if (!string.IsNullOrEmpty(searchingText))
                {
                    Venues = Venues.Where(p=> p.Name.ToLower().Contains(searchingText.ToLower()));
                }
                if (Venues is null || Venues.Count() <= 0)
                    throw new NotFoundException(nameof(Venue), searchingText);
                var paginatedVenues = await PaginatedList<Venue>.CreateAsync(Venues,pageNumber, pageSize);
                var venueResponses = _mapper.Map<List<GetAllVenueQueryResponse>>(paginatedVenues.Items);
                var result = new PaginatedList<GetAllVenueQueryResponse>(venueResponses, paginatedVenues.TotalCount, paginatedVenues.PageNumber, paginatedVenues.TotalPages);
                return result;
            }
        }
    }
    public class GetAllVenueQueryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Capacity { get; set; }
    }
}