using EventFlowAPI.DB.Entities;
using EventFlowAPI.DB.Entities.Abstract;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Mapper.Extensions;
using EventFlowAPI.Logic.Services.Interfaces;
using EventFlowAPI.Logic.Services.Services.BaseServices;
using EventFlowAPI.Logic.UnitOfWork;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace EventFlowAPI.Logic.Services.Services
{
    public sealed class EventService(IUnitOfWork unitOfWork) :
        GenericService<
            Event,
            EventRequestDto,
            EventResponseDto
            >(unitOfWork),
        IEventService
    {

        protected sealed override async Task<Error> ValidateEntity(EventRequestDto? requestDto, int? id = null)
        {
            var baseValidationError = await base.ValidateEntity(requestDto, id);

            if(baseValidationError != Error.None)
            {
                return baseValidationError;
            } 

            if (!await IsEntityExistInDB<EventCategory>(requestDto!.CategoryId))
            {
                return EventError.CategoryNotFound;
            }

            if (!await IsEntityExistInDB<Hall>(requestDto!.HallId))
            {
                return EventError.HallNotFound;
            }

            // !!!!! Event Time and Hall Validation 

            return Error.None;
        }


        protected sealed override IEntity PrepareEntityForAddition(Event entity)
        {
            entity.Duration = entity.EndDate - entity.StartDate;
            return entity;
        }


        protected sealed override IEnumerable<EventResponseDto> MapAsDto(IEnumerable<Event> records)
        {
            return records.Select(entity =>
            {
                var responseDto = entity.AsDto<EventResponseDto>();
                responseDto.Category = entity.Category?.AsDto<EventCategoryResponseDto>();
                responseDto.Details = entity.Details?.AsDto<EventDetailsResponseDto>();
                responseDto.Hall = entity.Hall?.AsDto<HallResponseDto>();
                return responseDto;
            });
        }


        protected sealed override EventResponseDto MapAsDto(Event entity)
        {
            var responseDto = entity.AsDto<EventResponseDto>();
            responseDto.Category = entity.Category?.AsDto<EventCategoryResponseDto>();
            responseDto.Details = entity.Details?.AsDto<EventDetailsResponseDto>();
            responseDto.Hall = entity.Hall?.AsDto<HallResponseDto>();
            responseDto.Tickets = entity.Tickets.Select(ticket =>
            {
                var ticketDto = ticket.AsDto<EventTicketResponseDto>();
                ticketDto.TicketType = ticket.TicketType.AsDto<TicketTypeResponseDto>();
                return ticketDto;               
            }).ToList();
            return responseDto;
        }
    }
}
