using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces;
using EventFlowAPI.Logic.Services.CRUDServices.Services.BaseServices;
using EventFlowAPI.Logic.UnitOfWork;

namespace EventFlowAPI.Logic.Services.CRUDServices.Services
{
    public sealed class TicketService(IUnitOfWork unitOfWork) :
        GenericService<
            Ticket,
            TicketRequestDto,
            TicketResponseDto
        >(unitOfWork),
        ITicketService
    {
        protected async sealed override Task<bool> IsSameEntityExistInDatabase(TicketRequestDto entityDto, int? id = null)
        {
            var entities = await _repository.GetAllAsync(q =>
                      q.Where(entity =>
                        entity.TicketTypeId == entityDto.TicketTypeId &&
                        entity.EventId == entityDto.EventId
                      )
                  );

            return IsEntityWithOtherIdExistInList(entities, id);
        }
    }
}
