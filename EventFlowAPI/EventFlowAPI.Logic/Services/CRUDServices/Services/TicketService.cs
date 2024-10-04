using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Mapper.Extensions;
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
        public async Task DeleteTickets(ICollection<Event> eventsToDelete, ICollection<Festival> festivalsToDelete)
        {
            var eventIds = eventsToDelete.Select(e => e.Id);
            var festivalIds = festivalsToDelete.Select(f => (int?)f.Id);

            var tickets = await _unitOfWork.GetRepository<Ticket>()
                                   .GetAllAsync(q => q.Where(t =>
                                   eventIds.Contains(t.EventId) ||
                                   festivalIds.Contains(t.FestivalId)));

            foreach (var ticket in tickets)
            {
                if (ticket.Reservations.Any())
                {
                    ticket.IsDeleted = true;
                    _unitOfWork.GetRepository<Ticket>().Update(ticket);
                }
                else
                {
                    _unitOfWork.GetRepository<Ticket>().Delete(ticket);
                }
            }
        }


        protected sealed override IEnumerable<TicketResponseDto> MapAsDto(IEnumerable<Ticket> records)
        {
            return records.Select(entity =>
            {
                var responseDto = entity.AsDto<TicketResponseDto>();
                responseDto.TicketType = entity.TicketType.AsDto<TicketTypeResponseDto>();
                responseDto.Event = entity.Event.AsDto<EventResponseDto>();
                responseDto.Event.Category = entity.Event.Category.AsDto<EventCategoryResponseDto>();
                //responseDto.Festival = entity.Festival?.AsDto<FestivalResponseDto>();
                return responseDto;
            });
        }
    

        /*protected sealed override TicketResponseDto MapAsDto(Ticket entity)
        {
            entity.AsDto<TicketResponseDto>();
        }*/
                                       

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
