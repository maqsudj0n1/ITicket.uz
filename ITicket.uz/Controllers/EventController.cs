using ITicket.uz.Application.Commons.Models;
using ITicket.uz.Application.UseCases.Events.Commands.CreateEvent;
using ITicket.uz.Application.UseCases.Events.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ITicket.uz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : BaseApiController
    {

        [HttpGet("[action]")]
        public async ValueTask<ActionResult<PaginatedList<GetAllEventsQueryResponse>>> GetAllEvents([FromQuery] GetAllEventsQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPost("[action]")]
        public async ValueTask<Guid> CreateEvent(CreateEventCommand command)
        {
            return await _mediator.Send(command);
        }

    }
}
