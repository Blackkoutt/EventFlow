using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Mapper.Extensions;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.Services.Interfaces;
using EventFlowAPI.Logic.Services.Services.BaseServices;
using EventFlowAPI.Logic.UnitOfWork;

namespace EventFlowAPI.Logic.Services.Services
{
    public sealed class HallTypeService(IUnitOfWork unitOfWork) : GenericService<HallType, HallTypeResponseDto>(unitOfWork), IHallTypeService
    {
        public override sealed async Task<Result<IEnumerable<HallTypeResponseDto>>> GetAllAsync()
        {
            //AutoMapperMappingException
            var records = await _repository.GetAllAsync();
            var result = records.Select(entity =>
            {
                var entityDto = entity.AsDto<HallTypeResponseDto>();
                entityDto.Equipments = entity.Equipments.Select(eq => eq.AsDto<EquipmentResponseDto>()).ToList();
                //entityDto.Equipments = entity.Equipments
                                          /*  .Select(eq =>
                                            {
                                                var eq1 = eq.Equipment;
                                                eq1.AsDto<EquipmentResponseDto>();
                                                eq.Equipment = eq1;
                                                return eq;
                                            })
                                            .ToList();*/
                //entityDto.Equipments = entity.Equipments.Select(eq => eq.Equipment.AsDto<EquipmentResponseDto>());
                return entityDto;
            });
            return Result<IEnumerable<HallTypeResponseDto>>.Success(result);
        }
    }
}
