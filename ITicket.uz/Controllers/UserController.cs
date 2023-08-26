using ITicket.uz.Application.Commons.Jwt.Models;
using ITicket.uz.Application.Commons.Models;
using ITicket.uz.Application.UseCases.Users.Commands.CreateUser;
using ITicket.uz.Application.UseCases.Users.Commands.LoginUser;
using ITicket.uz.Application.UseCases.Users.Commands.RegisterUser;
using ITicket.uz.Application.UseCases.Users.Queries.GetAllUser;
using ITicket.uz.Application.UseCases.Users.Response;
using Microsoft.AspNetCore.Mvc;

namespace ITicket.uz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        [HttpGet("[action]")]
        public async ValueTask<PaginatedList<UserResponse>> GetAllUser([FromQuery] GetAllUserQuery query)
          => await _mediator.Send(query);
        [HttpPost("[action]")]
        public async ValueTask<Guid> CreateUser([FromForm] CreateUserCommand command)
        => await _mediator.Send(command);

        [HttpPost("[action]")]
        public async ValueTask<TokenResponse> RegisterUser([FromForm] RegisterUserCommand command)
      => await _mediator.Send(command);


        [HttpPost("[action]")]
        public async ValueTask<TokenResponse> LoginUser([FromForm] LoginUserCommand command)
            => await _mediator.Send(command);

    }
}
