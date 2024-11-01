using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.DTO.UpdateRequestDto;
using EventFlowAPI.Logic.Query;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces.BaseInterfaces;

namespace EventFlowAPI.Logic.Services.CRUDServices.Interfaces
{
    public interface IEventService :
        IGenericService<
            Event,
            EventRequestDto,
            UpdateEventRequestDto,
            EventResponseDto,
            EventQuery
        >
    {
        Task<Result<(IEnumerable<Reservation> ReservationsForEvent, Event NewEvent, Event OldEvent)>> UpdateEvent(int id, UpdateEventRequestDto? requestDto); 
    }
}
