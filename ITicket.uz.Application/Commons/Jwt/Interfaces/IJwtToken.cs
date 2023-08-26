using ITicket.uz.Application.Commons.Jwt.Models;
using ITicket.uz.Domain.Identity;
using System.Security.Claims;
namespace ITicket.uz.Application.Commons.Jwt.Interfaces
{
    public interface IJwtToken
    {
        ValueTask<TokenResponse> CreateTokenAsync(string userName, string UserId, ICollection<Role> roles, CancellationToken cancellationToken = default);
        ValueTask<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token);
        ValueTask<string> GenerateRefreshTokenAsync(string userName);
    }
}
