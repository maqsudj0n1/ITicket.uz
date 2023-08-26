using ITicket.uz.Application.UseCases.Users.Commands.LoginUser;
using ITicket.uz.Domain.Identity;
namespace ITicket.uz.Application.Commons.Jwt.Interfaces
{
    public interface IUserRefreshToken
    {
        ValueTask<UserRefreshToken> AddOrUpdateRefreshToken(UserRefreshToken refreshToken, CancellationToken cancellationToken = default);
        ValueTask<User> AuthenAsync(LoginUserCommand user);
        ValueTask<bool> DeleteUserRefreshTokens(string username, string refreshToken, CancellationToken cancellationToken = default);
        ValueTask<UserRefreshToken> GetSavedRefreshTokens(string username, string refreshtoken);
    }

}
