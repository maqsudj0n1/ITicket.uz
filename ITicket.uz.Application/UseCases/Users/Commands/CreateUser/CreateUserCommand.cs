﻿using AutoMapper;
using ITicket.uz.Application.Commons.Exceptions;
using ITicket.uz.Application.Commons.Extensions;
using ITicket.uz.Application.Commons.Interfaces;
using ITicket.uz.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace ITicket.uz.Application.UseCases.Users.Commands.CreateUser
{
    public record CreateUserCommand : IRequest<Guid>
    {
        public string FullName { get; set; }
        public string Phone { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public Guid[]? RoleIds { get; set; }
        public IFormFile? ProfilePicture { get; set; }


    }
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CreateUserCommandHandler(IApplicationDbContext context, IMapper mapper, IConfiguration configuration)
        {
            (_context, _mapper, _configuration) = (context, mapper, configuration);
            
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            if (_context.Users.Any(x => x.Username == request.Username))
                throw new AlreadyExistsException(nameof(User), request.Username);



            var roles = await _context.Roles.ToListAsync(cancellationToken);



            var userRole = new List<Role>();
            if (request?.RoleIds?.Length > 0)
                roles.ForEach(role =>
                {
                    if (request.RoleIds.Any(id => id == role.Id))
                        userRole.Add(role);
                });


            var user = _mapper.Map<User>(request);
            user.Password= user.Password.GetHashedString();
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
            user.Roles = userRole;


            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            //await _cache.RemoveAsync(_configuration["RedisKey:User"], cancellationToken);
            return user.Id;


        }
    }

}