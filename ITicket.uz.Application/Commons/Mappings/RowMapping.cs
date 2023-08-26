using AutoMapper;
using ITicket.uz.Application.UseCases.Rows.Commands.CreateRow;
using ITicket.uz.Application.UseCases.Rows.Queries.GetAllRows;
using ITicket.uz.Domain.Entities;
namespace ITicket.uz.Application.Commons.Mappings;
public class RowMapping:Profile
{
    public RowMapping()
    {
        CreateMap<CreateRowCommand, Row>().ReverseMap();
        CreateMap<GetAllRowsQueryResponse, Row>().ReverseMap();
    }
}