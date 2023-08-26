using AutoMapper;
using ITicket.uz.Application.UseCases.Seats.Commands.CreateSeat;
using ITicket.uz.Application.UseCases.Seats.Queries;
using ITicket.uz.Domain.Entities;
namespace ITicket.uz.Application.Commons.Mappings;
public class SeatMapping:Profile
{
    public SeatMapping()
    {
        CreateMap<CreateSeatCommand, Seat>().ReverseMap();
        CreateMap<GetAllSeatsQueryResponse, Seat>().ReverseMap();
    }
}