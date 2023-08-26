using AutoMapper;
using ITicket.uz.Application.Commons.Exceptions;
using ITicket.uz.Application.Commons.Extensions;
using ITicket.uz.Application.Commons.Interfaces;
using ITicket.uz.Application.Commons.Jwt.Interfaces;
using ITicket.uz.Application.Commons.Jwt.Models;
using ITicket.uz.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
namespace ITicket.uz.Application.UseCases.Users.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<TokenResponse>
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public IFormFile? ProfilePicture { get; set; }
    }
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, TokenResponse>
    {
        private readonly IJwtToken _jwtToken;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;


        public RegisterUserCommandHandler(IJwtToken jwtToken, IApplicationDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _jwtToken = jwtToken;
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
         
        }
        public async Task<TokenResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var findUser = _context.Users.FirstOrDefault(x => x.Username == request.Username);
            if (_context.Users.Any(x => x.Username == request.Username))
            {
                var user = _mapper.Map<User>(request);
                user.Password = user.Password.GetHashedString();
                if (request.ProfilePicture != null)
                {
                    var picturepath = _configuration["UserPicturePath"];
                    string filename = user.Username + Path.GetExtension(request.ProfilePicture.FileName);
                    var userImagePath = Path.Combine(picturepath, filename);

                    using (var fs = new FileStream(userImagePath, FileMode.Create))
                    {
                        await request.ProfilePicture.CopyToAsync(fs);
                        user.Picture = userImagePath;
                    }
                }
                user.Id = findUser.Id;
                user.Roles = findUser.Roles;

                //await _context.Users.AddAsync(user, cancellationToken);
                //await _context.SaveChangesAsync(cancellationToken);
                var tokenResponse = await _jwtToken.CreateTokenAsync(user.Username, user.Id.ToString(), user.Roles, cancellationToken);
                // await _cache.RemoveAsync(_configuration["RedisKey:User"], cancellationToken);
                return tokenResponse;
            }
            throw new NotFoundException(nameof(User));
           
        }
    }
}