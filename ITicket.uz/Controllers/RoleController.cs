using ITicket.uz.Application.UseCases.Roles.Commands.CreateRole;
using ITicket.uz.Application.UseCases.Roles.Queries.GetAllRole;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITicket.uz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : BaseApiController
    {
        [HttpGet("[action]")]
        public async ValueTask<List<RoleResponse>> GetAllRoles()
       => await _mediator.Send(new GetAllRoleQuery());
        [HttpPost("[action]")]
        public async ValueTask<Guid> CreateRole(CreateRoleCommand command)
      => await _mediator.Send(command);
    }
}
