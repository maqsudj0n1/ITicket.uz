using ITicket.uz.Application.Commons.Models;
using ITicket.uz.Application.UseCases.Customers.Commands.CreateCustomer;
using ITicket.uz.Application.UseCases.Customers.Queries.GetAllCustomer;
using ITicket.uz.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace ITicket.uz.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize(Roles = "Admin")]
public class CustomerController : BaseApiController
{
   
    [HttpGet("[action]")]
    [LazyCache(5, 10)]
    [EnableRateLimiting("sliding")]
    public async ValueTask<ActionResult<PaginatedList<GetAllCustomersQueryResponse>>> GetAllCustomers([FromQuery] GetAllCustomersQuery query)
    {
        return await _mediator.Send(query);
    }
    [HttpPost("[action]")]
    public async ValueTask<Guid> CreateCustomer(CreateCustomerCommand command)
    {
        return await _mediator.Send(command);
    }
}
