using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Services.Interfaces;
using EventFlowAPI.Logic.Services.Services.BaseServices;
using EventFlowAPI.Logic.UnitOfWork;

namespace EventFlowAPI.Logic.Services.Services
{
    public sealed class EquipmentService(IUnitOfWork unitOfWork) :
        GenericService<
            Equipment,
            EquipmentRequestDto,
            EquipmentResponseDto
        >(unitOfWork),
        IEquipmentService
    {
    }
}
