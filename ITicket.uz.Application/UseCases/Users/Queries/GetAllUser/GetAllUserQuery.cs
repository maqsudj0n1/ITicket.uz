using AutoMapper;
using ITicket.uz.Application.Commons.Interfaces;
using ITicket.uz.Application.Commons.Models;
using ITicket.uz.Application.UseCases.Users.Response;
using ITicket.uz.Domain.Identity;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
namespace ITicket.uz.Application.UseCases.Users.Queries.GetAllUser;

public record GetAllUserQuery : IRequest<PaginatedList<UserResponse>>
{
    public string? SearchingText { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, PaginatedList<UserResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public GetAllUserQueryHandler(IApplicationDbContext context, IMapper mapper, IConfiguration configuration)
    {
        (_context, _mapper) = (context, mapper);
        _configuration = configuration;
    }

    public async Task<PaginatedList<UserResponse>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        var pageSize = request.PageSize;
        var pageNumber = request.PageNumber;
        var searchingText = request.SearchingText?.Trim();

        var users = _context.Users.ToList();
        var settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };


        if (!string.IsNullOrEmpty(searchingText))
        {
            users = users
                .Where(u => u.Username.ToLower().Contains(searchingText.ToLower())
                || u.FullName.ToLower().Contains(searchingText.ToLower())
                || u.Phone.ToLower().Contains(searchingText.ToLower())).ToList();
        }
        var paginatedUser = await PaginatedList<User>.CreateAsync(users, pageNumber, pageSize);
        var responseUser = _mapper.Map<List<UserResponse>>(paginatedUser.Items);
        var result = new PaginatedList<UserResponse>
            (responseUser, paginatedUser.TotalCount, request.PageNumber, request.PageSize);
        return result;
    }
}