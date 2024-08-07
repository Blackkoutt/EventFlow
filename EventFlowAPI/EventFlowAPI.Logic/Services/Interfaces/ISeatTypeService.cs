using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Services.Interfaces.BaseInterfaces;

namespace EventFlowAPI.Logic.Services.Interfaces
{
    public interface ISeatTypeService : IGenericService<SeatType, SeatTypeResponseDto>
    {
    }
}
