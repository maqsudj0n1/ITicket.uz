using AutoMapper;
using ITicket.uz.Application.UseCases.Users.Commands.CreateUser;
using ITicket.uz.Application.UseCases.Users.Commands.RegisterUser;
using ITicket.uz.Application.UseCases.Users.Response;
using ITicket.uz.Domain.Identity;
namespace ITicket.uz.Application.Commons.Mappings;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<UserResponse, User>().ReverseMap()
            .ForMember(dest => dest.RoleNames, opt => opt.MapFrom(src => src.Roles.Select(x => x.Name)));

        CreateMap<RegisterUserCommand, User>().ReverseMap();
        CreateMap<CreateUserCommand, User>().ReverseMap();

    }
}