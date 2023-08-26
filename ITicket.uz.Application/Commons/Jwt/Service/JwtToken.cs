using ITicket.uz.Application.Commons.Extensions;
using ITicket.uz.Application.Commons.Interfaces;
using ITicket.uz.Application.Commons.Jwt.Interfaces;
using ITicket.uz.Application.Commons.Jwt.Models;
using ITicket.uz.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ITicket.uz.Application.Commons.Jwt.Service
{
    public class JwtToken : IJwtToken
    {
        private readonly IConfiguration _configuration;
        private readonly IApplicationDbContext _context;

        public JwtToken(IConfiguration configuration, IApplicationDbContext context)
        {
            _configuration = configuration;
            _context=context;
        }

        public async ValueTask<TokenResponse> CreateTokenAsync(string userName, string UserId, ICollection<Role> Roles, CancellationToken cancellationToken = default)
        {
            var roles = await _context.Users.Include(x=> x.Roles).FirstOrDefaultAsync(user => user.Id == Guid.Parse(UserId));

            
            var claims = new List<Claim>()
            
            
        {
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.NameIdentifier,UserId),
            new Claim(ClaimTypes.Role, roles.Roles.FirstOrDefault().Name)

        };
            if (Roles.Count > 0)
            {

                foreach (var role in Roles)
                {
                    foreach (var permission in role.Permissions)
                    {
                        claims.Add(new Claim("permission", permission.Name));

                    }
                }
            }

            var jwt = new JwtSecurityToken(
                    issuer: _configuration.GetValue<string>("JWT:Issuer"),
                    audience: _configuration.GetValue<string>("JWT:Audience"),
                    claims: claims,
                    expires: DateTime.Now.AddDays(_configuration.GetValue<int>("JWT:TokenExpiredTimeAtDays", 10)),
                    signingCredentials: new SigningCredentials(
                            new SymmetricSecurityKey(
                                    Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWT:Key"))),
                                    SecurityAlgorithms.HmacSha256));

            var responseModel = new TokenResponse
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwt),
                RefreshToken = await GenerateRefreshTokenAsync(userName)
            };

            return responseModel;
        }

        public ValueTask<string> GenerateRefreshTokenAsync(string userName)
        {
            var refreshToken = (userName + DateTime.Now).GetHashedString();
            return new ValueTask<string>(refreshToken);
        }

        public ValueTask<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParametrs = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = false,
                ValidAudience = _configuration.GetValue<string>("JWT:Audience"),
                ValidIssuer = _configuration.GetValue<string>("JWT:Issuer"),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWT:Key")))

            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParametrs, out SecurityToken securityToken);
            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }
            return ValueTask.FromResult(principal);
        }
    }
}