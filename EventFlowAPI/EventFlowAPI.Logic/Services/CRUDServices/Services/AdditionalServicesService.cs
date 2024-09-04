using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces;
using EventFlowAPI.Logic.Services.CRUDServices.Services.BaseServices;
using EventFlowAPI.Logic.UnitOfWork;
using System.Runtime.CompilerServices;

namespace EventFlowAPI.Logic.Services.CRUDServices.Services
{
    public sealed class AdditionalServicesService(IUnitOfWork unitOfWork) :
        GenericService<
            AdditionalServices,
            AdditionalServicesRequestDto,
            AdditionalServicesResponseDto
        >(unitOfWork),
        IAdditionalServicesService
    {
        protected async sealed override Task<bool> IsSameEntityExistInDatabase(AdditionalServicesRequestDto entityDto, int? id = null)
        {
            var entities = await _repository.GetAllAsync(q =>
                      q.Where(entity => entity.Name == entityDto.Name));

            return IsEntityWithOtherIdExistInList(entities, id);
        }
    }
}
