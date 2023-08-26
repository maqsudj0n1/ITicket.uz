using ITicket.uz.Application.Commons.Models;
using ITicket.uz.Application.UseCases.Rows.Commands.CreateRow;
using ITicket.uz.Application.UseCases.Rows.Queries.GetAllRows;
using ITicket.uz.Application.UseCases.Venues.Commands.CreateVenue;
using ITicket.uz.Application.UseCases.Venues.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITicket.uz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RowController : BaseApiController
    {
        [HttpGet("[action]")]
        public async ValueTask<ActionResult<PaginatedList<GetAllRowsQueryResponse>>> GetAllRows([FromQuery] GetAllRowsQuery query)
        {
            return await _mediator.Send(query);
        }
        [HttpPost("[action]")]
        public async ValueTask<Guid> CreateRow(CreateRowCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
