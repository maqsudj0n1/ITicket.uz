using ITicket.uz.Application.Commons.Models;
using ITicket.uz.Application.UseCases.Reservations.Commands.CreateReservation;
using ITicket.uz.Application.UseCases.Reservations.Commands.ReturnTicket;
using ITicket.uz.Application.UseCases.Reservations.Commands.ReturnTicketCustomer;
using ITicket.uz.Application.UseCases.Reservations.Queries.GetAllReservation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITicket.uz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : BaseApiController
    {
        [HttpGet("[action]")]
        public async ValueTask<ActionResult<PaginatedList<GetAllReservationsQueryResponse>>> GetAllReservations([FromQuery] GetAllReservationsQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPost("[action]")]
        public async ValueTask<Guid> CreateReservation(CreateReservationCommand command)
        {
            return await _mediator.Send(command);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("[action]")]
        public async ValueTask<bool> CancelReservation(ReturnTicketCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpPost("[action]")]
        public async ValueTask<bool> CancelReservationCustomer(ReturnTicketCustomerCommand command)
        {
            return await _mediator.Send(command);
        }

    }
}
