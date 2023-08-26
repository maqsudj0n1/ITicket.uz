using ITicket.uz.Application.Commons.Exceptions;
using ITicket.uz.Application.Commons.Jwt.Interfaces;
using ITicket.uz.Application.Commons.Jwt.Models;
using MediatR;
namespace ITicket.uz.Application.UseCases.Users.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<TokenResponse>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, TokenResponse>
    {
        private readonly IJwtToken _jwtToken;
        private readonly IUserRefreshToken _userRefreshToken;

        public LoginUserCommandHandler(IJwtToken jwtToken, IUserRefreshToken userRefreshToken)
        {
            _jwtToken = jwtToken;
            _userRefreshToken = userRefreshToken;

        }

        public async Task<TokenResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var authenUser = await _userRefreshToken.AuthenAsync(request);
            if (authenUser is null)
                throw new UnauthorizedException(request.Username, request.Password);

            var tokenResponse = await _jwtToken.CreateTokenAsync(authenUser.Username, authenUser.Id.ToString(), authenUser.Roles, cancellationToken);

            return tokenResponse;
        }
    }

}