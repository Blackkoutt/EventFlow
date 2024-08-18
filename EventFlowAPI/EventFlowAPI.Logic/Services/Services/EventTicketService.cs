using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Services.Interfaces;
using EventFlowAPI.Logic.Services.Services.BaseServices;
using EventFlowAPI.Logic.UnitOfWork;

namespace EventFlowAPI.Logic.Services.Services
{
    public sealed class EventTicketService(IUnitOfWork unitOfWork) :
        GenericService<
            EventTicket,
            EventTicketRequestDto,
            EventTicketResponseDto
        >(unitOfWork),
        IEventTicketService
    {
        protected async sealed override Task<bool> IsSameEntityExistInDatabase(EventTicketRequestDto entityDto, int? id = null)
        {
            var entities = await _repository.GetAllAsync(q =>
                      q.Where(entity =>
                        entity.TicketTypeId == entityDto.TicketTypeId &&
                        entity.EventId == entityDto.EventId
                      )
                  );

            return base.IsEntityWithOtherIdExistInList(entities, id);
        }
    }
}
