using EventFlowAPI.DB.Entities;
using EventFlowAPI.DB.Entities.Abstract;
using EventFlowAPI.Logic.DTO.Interfaces;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Mapper.Extensions;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces;
using EventFlowAPI.Logic.Services.CRUDServices.Services.BaseServices;
using EventFlowAPI.Logic.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace EventFlowAPI.Logic.Services.CRUDServices.Services
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

            if (baseValidationError != Error.None)
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

            if (await CheckTimeCollisionsWithEvents(requestDto))
            {
                return EventError.CollisionWithExistingEvent;
            }

            if (await CheckTimeCollisionsWithHallRents(requestDto))
            {
                return EventError.CollisionWithExistingHallRent;
            }

            return Error.None;
        }


        protected sealed override Event PrepareEntityForAddOrUpdate(Event newEntity, EventRequestDto requestDto, Event? oldEntity = null)
        {
            if (oldEntity != null)
            {
                newEntity.DefaultHallId = oldEntity.HallId;
            }
            else
            {
                newEntity.DefaultHallId = newEntity.HallId;
            }
            newEntity.Duration = newEntity.EndDate - newEntity.StartDate;
            return newEntity;
        }

        protected sealed override Event MapAsEntity(EventRequestDto requestDto)
        {
            var eventEntity = base.MapAsEntity(requestDto);
            AddEventDetails(eventEntity, requestDto.LongDescription);
            return eventEntity;
        }
        protected sealed override Event PrepareEnityAfterAddition(Event entity)
        {
            var eventCopy = (Event)new Event().MakeCopyFrom(entity);
            eventCopy.Hall.Seats = [];
            eventCopy.Hall.Type = null!;
            return eventCopy;
        }



        protected sealed override IEntity MapToEntity(EventRequestDto requestDto, Event oldEntity)
        {
            var eventEntity = (Event)base.MapToEntity(requestDto, oldEntity);
            AddEventDetails(eventEntity, requestDto.LongDescription);
            return eventEntity;
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

        private async Task<bool> CheckTimeCollisionsWithHallRents(EventRequestDto newEntity)
        {
            return (await _unitOfWork.GetRepository<HallRent>()
                        .GetAllAsync(q =>
                            q.Where(entity =>
                                EF.Property<int>(entity, "DefaultHallId") == newEntity.HallId &&
                                (newEntity.StartDate <= EF.Property<DateTime>(entity, "StartDate") &&
                                 newEntity.EndDate > EF.Property<DateTime>(entity, "StartDate") ||
                                newEntity.StartDate < EF.Property<DateTime>(entity, "EndDate") &&
                                 newEntity.EndDate >= EF.Property<DateTime>(entity, "EndDate")))
                        )
                    ).Any();
        }
        private async Task<bool> CheckTimeCollisionsWithEvents(EventRequestDto newEntity)
        {
            return (await _unitOfWork.GetRepository<Event>()
                        .GetAllAsync(q =>
                            q.Where(entity =>
                                EF.Property<int>(entity, "DefaultHallId") == newEntity.HallId &&
                                (newEntity.StartDate <= EF.Property<DateTime>(entity, "StartDate") &&
                                 newEntity.EndDate > EF.Property<DateTime>(entity, "StartDate") ||
                                newEntity.StartDate < EF.Property<DateTime>(entity, "EndDate") &&
                                 newEntity.EndDate >= EF.Property<DateTime>(entity, "EndDate")))
                        )
                    ).Any();
        }

        /*private async Task<bool> CheckTimeCollisions<TEntity>(EventRequestDto newEntity) where TEntity : class
        {
            return (await _unitOfWork.GetRepository<TEntity>()
                        .GetAllAsync(q =>
                            q.Where(entity =>
                                EF.Property<int>(EF.Property<object>(entity, "Hall"), "DefaultHallId") == newEntity.HallId &&
                                ((newEntity.StartDate <= EF.Property<DateTime>(entity, "StartDate") &&
                                 newEntity.EndDate > EF.Property<DateTime>(entity, "StartDate")) ||
                                (newEntity.StartDate < EF.Property<DateTime>(entity, "EndDate") &&
                                 newEntity.EndDate >= EF.Property<DateTime>(entity, "EndDate"))))
                        )
                    ).Any();
        }*/

        private static void AddEventDetails(Event eventEntity, string? details)
        {
            if (details != null && details?.Trim() != string.Empty)
            {
                eventEntity.Details = new EventDetails { LongDescription = details };
            }
        }

        protected async sealed override Task<bool> IsSameEntityExistInDatabase(EventRequestDto entityDto, int? id = null)
        {
            var entities = await _repository.GetAllAsync(q =>
                      q.Where(entity =>
                        entity.Name == entityDto.Name &&
                        entity.EndDate >= DateTime.Now &&
                        entity.CategoryId == entityDto.CategoryId
                      ));

            return IsEntityWithOtherIdExistInList(entities, id);
        }
    }
}
