using AutoMapper;
using ITicket.uz.Application.UseCases.Customers.Commands.CreateCustomer;
using ITicket.uz.Application.UseCases.Customers.Queries.GetAllCustomer;
using ITicket.uz.Application.UseCases.Rows.Queries.GetAllRows;
using ITicket.uz.Domain.Entities;
namespace ITicket.uz.Application.Commons.Mappings;
public class CustomerMapping:Profile
{
    public CustomerMapping()
    {
        CreateMap<CreateCustomerCommand, Customer>().ReverseMap();
        CreateMap<GetAllCustomersQueryResponse, Customer>().ReverseMap();
    }
}