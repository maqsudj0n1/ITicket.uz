using AutoMapper;
using ITicket.uz.Application.Commons.Exceptions;
using ITicket.uz.Application.Commons.Interfaces;
using ITicket.uz.Application.UseCases.Users.Response;
using ITicket.uz.Domain.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITicket.uz.Application.UseCases.Users.Queries.GetById
{
    public record GetByIdUserQuery(Guid Id) : IRequest<UserResponse>;


    public class GetByIdUserResponse : IRequestHandler<GetByIdUserQuery, UserResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetByIdUserResponse(IApplicationDbContext context, IMapper mapper)
               => (_context, _mapper) = (context, mapper);

        public async Task<UserResponse> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            var foundUser = await _context.Users.FindAsync(new object[] { request.Id }, cancellationToken);
            if (foundUser is null)
                throw new NotFoundException(nameof(User), request.Id);
            var result = _mapper.Map<UserResponse>(foundUser);
            return result;
        }
    }
}