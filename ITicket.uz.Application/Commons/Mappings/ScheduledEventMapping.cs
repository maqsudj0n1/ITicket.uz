using AutoMapper;
using ITicket.uz.Application.UseCases.ScheduledEvents.Commands.CreateScheduledEvent;
using ITicket.uz.Application.UseCases.ScheduledEvents.Queries.GetAllScheduledEvent;
using ITicket.uz.Domain.Entities;
namespace ITicket.uz.Application.Commons.Mappings;
public class ScheduledEventMapping:Profile
{
    public ScheduledEventMapping()
    {
        CreateMap<CreateScheduledEventCommand, ScheduledEvent>().ReverseMap();
        CreateMap<GetAllScheduledEventQueryResponse,  ScheduledEvent>().ReverseMap();
    }
}