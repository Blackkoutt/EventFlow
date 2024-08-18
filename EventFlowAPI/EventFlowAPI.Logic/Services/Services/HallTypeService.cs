using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Mapper.Extensions;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.Services.Interfaces;
using EventFlowAPI.Logic.Services.Services.BaseServices;
using EventFlowAPI.Logic.UnitOfWork;

namespace EventFlowAPI.Logic.Services.Services
{
    public sealed class HallTypeService(IUnitOfWork unitOfWork) :
        GenericService<
            HallType,
            HallTypeRequestDto,
            HallTypeResponseDto
        >(unitOfWork),
        IHallTypeService
    {
        protected async sealed override Task<bool> IsSameEntityExistInDatabase(HallTypeRequestDto entityDto, int? id = null)
        {
            var entities = await _repository.GetAllAsync(q =>
                q.Where(entity =>
                    entity.Name == entityDto.Name &&
                    (entityDto.EquipmentIds == null || entity.Equipments.All(eq => entityDto.EquipmentIds.Contains(eq.Id)))
                )
            );

            return base.IsEntityWithOtherIdExistInList(entities, id);
        }

        protected sealed override HallTypeResponseDto MapAsDto(HallType entity)
        {
            var responseDto = entity.AsDto<HallTypeResponseDto>();
            responseDto.Equipments = entity.Equipments.Select(eq => eq.AsDto<EquipmentResponseDto>()).ToList();
            return responseDto;
        }
        protected sealed override async Task<Error> ValidateEntity(HallTypeRequestDto? requestDto, int? id = null)
        {
            var baseValidationError = await base.ValidateEntity(requestDto, id);
            if (baseValidationError != Error.None)
            {
                return baseValidationError;
            }
            if(requestDto!.EquipmentIds == null)
            {
                return HallTypeError.NullEquipmentsParameter;
            }
            foreach(int equipmentId in requestDto!.EquipmentIds!)
            {
                if (!await IsEntityExistInDB<Equipment>(equipmentId))
                {
                    return HallTypeError.EquipmentNotFound;
                }
            }

            return Error.None;
        }
    }
}
