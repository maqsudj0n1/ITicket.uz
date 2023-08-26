using AutoMapper;
using ITicket.uz.Application.UseCases.Reservations.Commands.CreateReservation;
using ITicket.uz.Application.UseCases.Reservations.Queries.GetAllReservation;
using ITicket.uz.Domain.Entities;
namespace ITicket.uz.Application.Commons.Mappings;
public class ReservationMapping:Profile
{
    public ReservationMapping()
    {
        CreateMap<CreateReservationCommand, Reservation>().ReverseMap();
        CreateMap<GetAllReservationsQueryResponse, Reservation>().ReverseMap();
    }
}