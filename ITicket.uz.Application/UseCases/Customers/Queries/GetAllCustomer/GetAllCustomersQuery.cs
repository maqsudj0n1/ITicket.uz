using AutoMapper;
using ITicket.uz.Application.Commons.Exceptions;
using ITicket.uz.Application.Commons.Interfaces;
using ITicket.uz.Application.Commons.Models;
using ITicket.uz.Domain.Entities;
using MediatR;
namespace ITicket.uz.Application.UseCases.Customers.Queries.GetAllCustomer;
public class GetAllCustomersQuery : IRequest<PaginatedList<GetAllCustomersQueryResponse>>
{
    public string? SearchingText { get; set; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomersQuery, PaginatedList<GetAllCustomersQueryResponse>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public GetAllCustomerQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<GetAllCustomersQueryResponse>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var pageSize = request.PageSize;
        var pageNumber = request.PageNumber;
        var searchingText = request.SearchingText?.Trim();

        var customers = _context.Customers.AsQueryable();
        if (!string.IsNullOrEmpty(searchingText))
        {
            customers = customers.Where(p => p.FirstName.ToLower().Contains(searchingText.ToLower()));
        }
        if (customers is null || customers.Count() <= 0)
            throw new NotFoundException(nameof(Customer), searchingText);
        var paginatedCustomers = await PaginatedList<Customer>.CreateAsync(customers, pageNumber, pageSize);

        var customerResponses = _mapper.Map<List<GetAllCustomersQueryResponse>>(paginatedCustomers.Items);

        var result = new PaginatedList<GetAllCustomersQueryResponse>(customerResponses, paginatedCustomers.TotalCount,request.PageNumber, request.PageSize);
        return result;

    }
}
public class GetAllCustomersQueryResponse
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}