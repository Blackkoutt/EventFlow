using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Services.Interfaces;
using EventFlowAPI.Logic.Services.Services.BaseServices;
using EventFlowAPI.Logic.UnitOfWork;

namespace EventFlowAPI.Logic.Services.Services
{
    public sealed class HallRentService(IUnitOfWork unitOfWork) :
        GenericService<
            HallRent,
            HallRentRequestDto,
            HallRentResponseDto
        >(unitOfWork),
        IHallRentService
    {
        protected sealed override HallRent PrepareEntityForAddOrUpdate(HallRent newEntity, HallRentRequestDto requestDto, HallRent? oldEntity = null)
        {
            if (oldEntity != null)
            {
                newEntity.DefaultHallId = oldEntity.HallId;
            }
            else
            {
                newEntity.DefaultHallId = newEntity.HallId;
            }
            return newEntity;
        }
        protected sealed override Task<bool> IsSameEntityExistInDatabase(HallRentRequestDto entityDto, int? id = null)
        {
            throw new NotImplementedException();
        }
    }
}
