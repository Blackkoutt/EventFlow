using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces.BaseInterfaces;

namespace EventFlowAPI.Logic.Services.CRUDServices.Interfaces
{
    public interface IEventPassService :
        IGenericService<
            EventPass,
            EventPassRequestDto,
            EventPassResponseDto
        >
    {
        Task<Result<EventPassResponseDto>> BuyEventPass(EventPassRequestDto? requestDto);
    }
}
