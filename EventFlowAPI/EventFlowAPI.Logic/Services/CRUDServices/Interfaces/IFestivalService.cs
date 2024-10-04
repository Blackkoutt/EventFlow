using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces.BaseInterfaces;

namespace EventFlowAPI.Logic.Services.CRUDServices.Interfaces
{
    public interface IFestivalService :
        IGenericService<
            Festival,
            FestivalRequestDto,
            FestivalResponseDto
        >
    {
        Task<ICollection<Festival>> CancelFestivalIfEssential(IEnumerable<Event> eventsToDelete);
    }
}
