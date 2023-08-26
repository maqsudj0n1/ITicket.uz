using AutoMapper;
using ITicket.uz.Application.UseCases.Venues.Commands.CreateVenue;
using ITicket.uz.Application.UseCases.Venues.Queries;
using ITicket.uz.Domain.Entities;
namespace ITicket.uz.Application.Commons.Mappings;
public class VenueMapping:Profile
{
    public VenueMapping()
    {
        CreateMap<CreateVenueCommand, Venue>().ReverseMap();
        CreateMap<GetAllVenueQueryResponse, Venue>().ReverseMap();
    }
}