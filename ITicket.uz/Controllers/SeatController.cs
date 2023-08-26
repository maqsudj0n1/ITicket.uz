using ITicket.uz.Application.Commons.Models;
using ITicket.uz.Application.UseCases.Seats.Commands.CreateSeat;
using ITicket.uz.Application.UseCases.Seats.Queries;
using ITicket.uz.Services;
using Microsoft.AspNetCore.Mvc;
namespace ITicket.uz.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SeatController : BaseApiController
{
    [LazyCache(5,10)]
    [HttpGet("[action]")]
    public async ValueTask<ActionResult<PaginatedList<GetAllSeatsQueryResponse>>> GetAllSeats([FromQuery] GetAllSeatsQuery query)
    {
        return await _mediator.Send(query);
    }
    [HttpGet("[action]")]
    public async ValueTask<ActionResult<List<GetAllEmptySeatsQueryResponse>>> GetAllEmptySeats([FromQuery] GetAllEmptySeatsQuery query)
    {
        return await _mediator.Send(query);
    }
    [HttpPost("[action]")]
    public async ValueTask<Guid> CreateSeat(CreateSeatCommand command)
    {
        return await _mediator.Send(command);
    }
}
