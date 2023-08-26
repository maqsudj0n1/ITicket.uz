using ITicket.uz.Application.Commons.Models;
using ITicket.uz.Application.UseCases.Permissions.Queries.GetAllPermission;
using ITicket.uz.Application.UseCases.Permissions;
using Microsoft.AspNetCore.Mvc;
using ITicket.uz.Application.UseCases.Permissions.Commands.CreatePermission;

namespace ITicket.uz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : BaseApiController
    {
        [HttpGet("[action]")]
        public async ValueTask<PaginatedList<PermissionResponse>> GetAllPermissions([FromQuery] GetAllPermissionQuery query)
       => await _mediator.Send(query);
        [HttpPost("[action]")]
        public async ValueTask<List<PermissionResponse>> CreatePermission(CreatePermissionCommand command)
       => await _mediator.Send(command);
    }
}
