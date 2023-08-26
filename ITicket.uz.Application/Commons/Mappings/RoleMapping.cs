using AutoMapper;
using ITicket.uz.Application.UseCases.Roles.Queries.GetAllRole;
using ITicket.uz.Domain.Identity;

namespace ITicket.uz.Application.Commons.Mappings
{
    public class RoleMapping : Profile
    {
        public RoleMapping()
        {
            CreateMap<RoleResponse, Role>().ReverseMap();
        }
    }
}