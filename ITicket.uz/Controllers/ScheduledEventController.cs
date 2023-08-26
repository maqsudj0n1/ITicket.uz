using ITicket.uz.Application.Commons.Models;
using ITicket.uz.Application.UseCases.ScheduledEvents.Commands.CreateScheduledEvent;
using ITicket.uz.Application.UseCases.ScheduledEvents.Queries.GetAllScheduledEvent;
using Microsoft.AspNetCore.Mvc;

namespace ITicket.uz.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ScheduledEventController:BaseApiController
{

    [HttpGet("[action]")]
    public async ValueTask<ActionResult<PaginatedList<GetAllScheduledEventQueryResponse>>> GetAllScheduledEvents([FromQuery] GetAllScheduledEventQuery query)
    {
        return await _mediator.Send(query);
    }

    [HttpPost("[action]")]
    public async ValueTask<Guid> CreateScheduledEvent(CreateScheduledEventCommand command)
    {
        return await _mediator.Send(command);
    }

}
