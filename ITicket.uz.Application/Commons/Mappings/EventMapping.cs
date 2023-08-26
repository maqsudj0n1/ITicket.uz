using AutoMapper;
using ITicket.uz.Application.UseCases.Events.Commands.CreateEvent;
using ITicket.uz.Application.UseCases.Events.Queries;
using ITicket.uz.Domain.Entities;

namespace ITicket.uz.Application.Commons.Mappings
{
    public class EventMapping:Profile
    {
        public EventMapping()
        {
            CreateMap<CreateEventCommand, Event>().ReverseMap();
            CreateMap<GetAllEventsQueryResponse, Event>().ReverseMap();
        }
    }
}