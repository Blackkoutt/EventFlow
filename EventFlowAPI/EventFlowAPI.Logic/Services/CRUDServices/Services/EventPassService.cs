using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces;
using EventFlowAPI.Logic.Services.CRUDServices.Services.BaseServices;
using EventFlowAPI.Logic.UnitOfWork;

namespace EventFlowAPI.Logic.Services.CRUDServices.Services
{
    public sealed class EventPassService(IUnitOfWork unitOfWork) :
        GenericService<
            EventPass,
            EventPassRequestDto,
            EventPassResponseDto
        >(unitOfWork),
        IEventPassService
    {
        protected sealed override Task<bool> IsSameEntityExistInDatabase(EventPassRequestDto entityDto, int? id = null)
        {
            throw new NotImplementedException();
        }
    }
}
