using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces.BaseInterfaces;

namespace EventFlowAPI.Logic.Services.CRUDServices.Interfaces
{
    public interface IAdditionalServicesService :
        IGenericService<
            AdditionalServices,
            AdditionalServicesRequestDto,
            AdditionalServicesResponseDto
        >
    {
    }
}
