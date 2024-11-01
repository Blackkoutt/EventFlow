using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.DTO.UpdateRequestDto;
using EventFlowAPI.Logic.Query;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces.BaseInterfaces;

namespace EventFlowAPI.Logic.Services.CRUDServices.Interfaces
{
    public interface IEventPassTypeService :
        IGenericService<
            EventPassType,
            EventPassTypeRequestDto,
            UpdateEventPassTypeRequestDto,
            EventPassTypeResponseDto,
            EventPassTypeQuery
        >
    {
    }
}
