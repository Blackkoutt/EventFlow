using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.ResultObject;
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
        Task DeleteTickets(ICollection<Event> eventsToDelete, ICollection<Festival> festivalsToDelete);
    }
}
