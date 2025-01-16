using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.DTO.UpdateRequestDto;
using EventFlowAPI.Logic.Query;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces.BaseInterfaces;

namespace EventFlowAPI.Logic.Services.CRUDServices.Interfaces
{
    public interface IHallTypeService :
        IGenericService<
            HallType,
            HallTypeRequestDto,
            UpdateHallTypeRequestDto,
            HallTypeResponseDto,
            HallTypeQuery
        >
    {
        IEnumerable<HallTypeResponseDto> MapAsDto(IEnumerable<HallType> records);
        HallTypeResponseDto MapAsDto(HallType entity);
    }
}
