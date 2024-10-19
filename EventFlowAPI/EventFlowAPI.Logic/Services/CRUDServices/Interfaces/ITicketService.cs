using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces.BaseInterfaces;

namespace EventFlowAPI.Logic.Services.CRUDServices.Interfaces
{
    public interface ITicketService :
        IGenericService<
            Ticket,
            TicketRequestDto,
            TicketResponseDto
        >
    {
        Task UpdateTicketsForFestival(ICollection<Event_FestivalTicketRequestDto> newFestivalTickets, Festival festival);
        Task UpdateTicketsForEvent(ICollection<Event_FestivalTicketRequestDto> newEventTickets, Event oldEvent);
        Task DeleteTickets(ICollection<Event> eventsToDelete, ICollection<Festival> festivalsToDelete);
        ICollection<Ticket> GetEventTickets(ICollection<Event_FestivalTicketRequestDto> ticketsDto);
        ICollection<Ticket> GetFestivalTickets(ICollection<Event_FestivalTicketRequestDto> ticketsDto, ICollection<Event> festivalEventList);
    }
}
