using AutoMapper;
using ITicket.uz.Application.Commons.Interfaces;
using MediatR;
using ITicket.uz.Domain.Entities;
namespace ITicket.uz.Application.UseCases.Customers.Commands.CreateCustomer;
public class CreateCustomerCommand:IRequest<Guid>
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Guid>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateCustomerCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        Customer customer = _mapper.Map<Customer>(request);

        await _dbContext.Customers.AddAsync(customer, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return customer.Id;
    }
}