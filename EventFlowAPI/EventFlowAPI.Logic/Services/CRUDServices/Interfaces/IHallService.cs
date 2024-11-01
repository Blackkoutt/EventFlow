using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.DTO.UpdateRequestDto;
using EventFlowAPI.Logic.Query;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces.BaseInterfaces;

namespace EventFlowAPI.Logic.Services.CRUDServices.Interfaces
{
    public interface IHallService :
        IGenericService<
            Hall,
            HallRequestDto,
            UpdateHallRequestDto,
            HallResponseDto,
            HallQuery
        >
    {
        Task<Result<HallResponseDto>> UpdateHallForEvent(int eventId, EventHallRequestDto? requestDto);
        Task<Result<HallResponseDto>> UpdateHallForRent(int rentId, HallRent_HallRequestDto? requestDto);
    }
}
