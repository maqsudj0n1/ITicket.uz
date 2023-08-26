using AutoMapper;
using ITicket.uz.Application.Commons.Extensions;
using ITicket.uz.Application.Commons.Interfaces;
using ITicket.uz.Application.Commons.Jwt.Interfaces;
using ITicket.uz.Application.UseCases.Users.Commands.LoginUser;
using ITicket.uz.Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace ITicket.uz.Application.Commons.Jwt.Service
{
    public class RefreshToken : IUserRefreshToken
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RefreshToken(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async ValueTask<UserRefreshToken> AddOrUpdateRefreshToken(UserRefreshToken refreshToken, CancellationToken cancellationToken = default)
        {
            var foundRefreshtoken = await _context.UserRefreshTokens.FirstOrDefaultAsync(x => x.UserName == refreshToken.UserName, cancellationToken);
            if (foundRefreshtoken is null)
            {
                await _context.UserRefreshTokens.AddAsync(refreshToken, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return refreshToken;
            }
            else
            {
                foundRefreshtoken.RefreshToken = refreshToken.RefreshToken;
                foundRefreshtoken.ExpiresTime = refreshToken.ExpiresTime;
                _context.UserRefreshTokens.Update(foundRefreshtoken);
                await _context.SaveChangesAsync(cancellationToken);
                return refreshToken;
            }
        }

        public async ValueTask<User> AuthenAsync(LoginUserCommand user)
        {
            string hashPassword = user.Password.GetHashedString();
            User? foundUser = await _context.Users.SingleOrDefaultAsync(x => x.Username == user.Username && x.Password == hashPassword);
            if (foundUser is null)
            {
                return null;
            }



            return foundUser;
        }

        public async ValueTask<bool> DeleteUserRefreshTokens(string username, string refreshToken, CancellationToken cancellationToken = default)
        {
            var foundRefreshToken = await _context.UserRefreshTokens.FirstOrDefaultAsync(x => x.UserName == username && x.RefreshToken == refreshToken);
            _context.UserRefreshTokens.Remove(foundRefreshToken);
            return (await _context.SaveChangesAsync(cancellationToken)) > 0;
        }

        public async ValueTask<UserRefreshToken> GetSavedRefreshTokens(string username, string refreshtoken)
        {
            var foundRefreshToken = await _context.UserRefreshTokens.FirstOrDefaultAsync(x => x.UserName == username && x.RefreshToken == refreshtoken);
            return foundRefreshToken;
        }
    }

}