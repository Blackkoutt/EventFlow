using EventFlowAPI.DB.Entities;
using EventFlowAPI.DB.Entities.Abstract;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Mapper.Extensions;
using EventFlowAPI.Logic.Query.Abstract;
using EventFlowAPI.Logic.Query;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces;
using EventFlowAPI.Logic.Services.CRUDServices.Services.BaseServices;
using EventFlowAPI.Logic.UnitOfWork;
using EventFlowAPI.Logic.Extensions;
using EventFlowAPI.Logic.Helpers;
using EventFlowAPI.Logic.Identity.Helpers;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces;

namespace EventFlowAPI.Logic.Services.CRUDServices.Services
{
    public sealed class EventService(
        IUnitOfWork unitOfWork,
        IHallService hallService,
        IReservationService reservationService,
        IUserService userService,
        IEmailSenderService emailSender,
        ICollisionCheckerService collisionChecker
        ) :
        GenericService<
            Event,
            EventRequestDto,
            EventResponseDto
            >(unitOfWork),
        IEventService
    {

        private readonly IHallService _hallService = hallService;
        private readonly IUserService _userService = userService;
        private readonly IEmailSenderService _emailSender = emailSender;
        private readonly IReservationService _reservationService = reservationService;
        private readonly ICollisionCheckerService _collisionChecker = collisionChecker;

        public sealed override async Task<Result<IEnumerable<EventResponseDto>>> GetAllAsync(QueryObject query)
        {
            var eventQuery = query as EventQuery;
            if (eventQuery == null)
                return Result<IEnumerable<EventResponseDto>>.Failure(QueryError.BadQueryObject);

            var records = await _repository.GetAllAsync(q => 
                                                q.EventByStatus(eventQuery.Status)
                                                .SortBy(eventQuery.SortBy, eventQuery.SortDirection));

            var response = MapAsDto(records);

            return Result<IEnumerable<EventResponseDto>>.Success(response);
        }

        public sealed override async Task<Result<EventResponseDto>> AddAsync(EventRequestDto? requestDto)
        {
            var validationError = await ValidateEntity(requestDto);
            if (validationError != Error.None)
                return Result<EventResponseDto>.Failure(validationError);

            var eventEntity = MapAsEntity(requestDto!);

            eventEntity.Duration = eventEntity.EndDate - eventEntity.StartDate;
            eventEntity.AddDate = DateTime.Now;

            // Make copy of hall
            var hallCopyResult = await _hallService.MakeCopyOfHall(requestDto!.HallId);
            if (!hallCopyResult.IsSuccessful)
                return Result<EventResponseDto>.Failure(hallCopyResult.Error);

            eventEntity.HallId = hallCopyResult.Value.Id;
            AddTicketsForEvent(requestDto.EventTickets, eventEntity);

            await _repository.AddAsync(eventEntity);
            await _unitOfWork.SaveChangesAsync();

            var response = MapAsDto(eventEntity);

            return Result<EventResponseDto>.Success(response);
        }


        public sealed override async Task<Result<EventResponseDto>> UpdateAsync(int id, EventRequestDto? requestDto)
        {
            var validationError = await ValidateEntity(requestDto, id);
            if (validationError != Error.None)
                return Result<EventResponseDto>.Failure(validationError);

            var eventEntity = await _repository.GetOneAsync(id);
            if (eventEntity == null)
                return Result<EventResponseDto>.Failure(Error.NotFound);

            var reservationsForEvent = await _reservationService.GetActiveReservationsForEvent(eventEntity.Id);

            var oldEventEntity = new Event
            {
                Name = eventEntity.Name,
                StartDate = eventEntity.StartDate,
                EndDate = eventEntity.EndDate,
                Hall = eventEntity.Hall,
                Tickets = eventEntity.Tickets
            };

            var newEventEntity = (Event)MapToEntity(requestDto!, eventEntity);

            bool isAnyDateInEventChanged = newEventEntity!.StartDate != oldEventEntity.StartDate ||
                                           newEventEntity!.EndDate != oldEventEntity.EndDate;

            if (isAnyDateInEventChanged)
            {
                newEventEntity.Duration = newEventEntity.EndDate - newEventEntity.StartDate;

                UpdateStartAndEndDateOfReservations(
                    reservationsForEvent: reservationsForEvent,
                    startDate: newEventEntity.StartDate,
                    endDate: newEventEntity.EndDate);

                await UpdateStartAndEndDateOfFestival(
                    eventId: eventEntity.Id,
                    startDate: newEventEntity.StartDate,
                    endDate: newEventEntity.EndDate);
            }
            
            if (newEventEntity.HallId != oldEventEntity.HallId)
            {
                var newHallResult = await GetNewEventHallAndUpdateReservationSeats(
                                        newHallId: requestDto!.HallId,
                                        reservationsForEvent: reservationsForEvent);
                if(!newHallResult.IsSuccessful)
                    return Result<EventResponseDto>.Failure(newHallResult.Error);

                newEventEntity.HallId = newHallResult.Value.Id;
            }

            await UpdateTicketsForEvent(requestDto!.EventTickets, oldEventEntity);

            _repository.Update(newEventEntity);
            await _unitOfWork.SaveChangesAsync();

            if (reservationsForEvent.Any())
            {
                var updatedProperties = await GetInfoAboutUpdatedEventProperties(oldEventEntity, newEventEntity);
                // info about changed seats
                if (updatedProperties.SendMailAboutUpdatedEvent)
                    await _reservationService.SendMailsAboutUpdatedReservations(reservationsForEvent, updatedProperties);
            }
            var newEventEntityDto = MapAsDto(newEventEntity);

            return Result<EventResponseDto>.Success(newEventEntityDto);
        }

        public sealed override async Task<Result<object>> DeleteAsync(int id)
        {
            if (id < 0)
                return Result<object>.Failure(Error.RouteParamOutOfRange);

            var eventEntity = await _repository.GetOneAsync(id);
            if (eventEntity == null)
                return Result<object>.Failure(Error.NotFound);

            if (eventEntity.IsCanceled)
                return Result<object>.Failure(EventError.EventIsCanceled);

            if (eventEntity.IsExpired)
                return Result<object>.Failure(EventError.EventIsExpired);

            var userResult = await _userService.GetCurrentUser();
            if (!userResult.IsSuccessful)
                return Result<object>.Failure(userResult.Error);

            var user = userResult.Value;
            if (!user.IsInRole(Roles.Admin))
                return Result<object>.Failure(AuthError.UserDoesNotHavePremissionToResource);

            var reservationsForEvent = await _reservationService.GetActiveReservationsForEvent(eventEntity.Id);
            if (reservationsForEvent.Any())
            {
                var cancelError = await CancelReservationsForEvent(reservationsForEvent, eventEntity);
                if(cancelError != Error.None)
                    return Result<object>.Failure(cancelError);

                await SoftDeleteTicketsForEvent(eventEntity.Id);

                eventEntity.CancelDate = DateTime.Now;
                eventEntity.IsCanceled = true;
                _repository.Update(eventEntity);
            }
            else
            {
                _unitOfWork.GetRepository<Hall>().Delete(eventEntity.Hall);
                _repository.Delete(eventEntity);   
            }
            await _unitOfWork.SaveChangesAsync();

            return Result<object>.Success();
        }


        private async Task SoftDeleteTicketsForEvent(int eventId)
        {
            var eventTickets = await _unitOfWork.GetRepository<Ticket>()
                                   .GetAllAsync(q => q.Where(t => t.EventId == eventId));

            foreach (var eventTicket in eventTickets)
            {
                eventTicket.IsDeleted = true;
                _unitOfWork.GetRepository<Ticket>().Update(eventTicket);
            }
        }


        private async Task<Error> CancelReservationsForEvent(IEnumerable<Reservation> reservationsForEvent, Event eventEntity)
        {
            var reservationsGroupByUser = reservationsForEvent.GroupBy(r => r.UserId)
                                            .Select(g => new
                                            {
                                                UserId = g.Key,
                                                ReservationList = g.ToList(),
                                            });

            foreach (var group in reservationsGroupByUser)
            {
                foreach (var reservation in group.ReservationList)
                {
                    var deleteError = await _reservationService.SoftDeleteReservation(reservation);
                    if (deleteError != Error.None)
                        return deleteError;
                }
                await _emailSender.SendInfoAboutCanceledEvents(group.ReservationList, eventEntity);
            }
            return Error.None;
        }

        private void UpdateStartAndEndDateOfReservations(IEnumerable<Reservation> reservationsForEvent, DateTime startDate, DateTime endDate)
        {
            if (reservationsForEvent.Any())
            {
                foreach (var reservation in reservationsForEvent)
                {
                    reservation.StartOfReservationDate = startDate;
                    reservation.EndOfReservationDate = endDate;
                    _unitOfWork.GetRepository<Reservation>().Update(reservation);
                }
            }
        }

        private async Task UpdateStartAndEndDateOfFestival(int eventId, DateTime startDate, DateTime endDate)
        {
            var eventFestivals = await _unitOfWork.GetRepository<Festival>()
                                        .GetAllAsync(q =>
                                        q.Where(f =>
                                        f.Events.Any(e =>
                                        e.Id == eventId)));

            if (eventFestivals.Any())
            {
                foreach (var festival in eventFestivals)
                {
                    bool isFestivalDateChanged = false;

                    if (startDate < festival.StartDate)
                    {
                        festival.StartDate = startDate;
                        isFestivalDateChanged = true;
                    }
                    if (endDate > festival.EndDate)
                    {
                        festival.EndDate = endDate;
                        isFestivalDateChanged = true;
                    }
                    if (isFestivalDateChanged)
                    {
                        festival.Duration = festival.EndDate - festival.StartDate;
                        _unitOfWork.GetRepository<Festival>().Update(festival);
                    }
                }
            }
        }

        private async Task<Result<Hall>> GetNewEventHallAndUpdateReservationSeats(int newHallId, IEnumerable<Reservation> reservationsForEvent)
        {
            var hallCopyResult = await _hallService.MakeCopyOfHall(newHallId);
            if (!hallCopyResult.IsSuccessful)
                return Result<Hall>.Failure(hallCopyResult.Error);

            var newHall = hallCopyResult.Value;

            if (reservationsForEvent.Count() > newHall.Seats.Count)
                return Result<Hall>.Failure(HallError.HallIsTooSmall);

            if (reservationsForEvent.Any())
            {
                var newHallSeatsRows = newHall.HallDetails!.NumberOfSeatsRows;
                var oldHall = reservationsForEvent.First().Ticket.Event.Hall;
                var oldHallSeatsRows = oldHall.HallDetails!.NumberOfSeatsRows;
                var rowRatio = (double)newHallSeatsRows / oldHallSeatsRows;

                var groupedReservations = reservationsForEvent.GroupBy(r => r.UserId);
                var availableSeats = newHall.Seats
                                        .OrderBy(s => s.Row)
                                        .ThenBy(s => s.Column)
                                        .ToDictionary(s => (s.Row, s.Column), s => s);


                Dictionary<Reservation, List<Seat>> reservationsSeatsDict = [];
                foreach (var group in groupedReservations)
                {
                    var firstSeat = group.First().Seats.First();
                    int targetRow = (int)Math.Round(firstSeat.Row * rowRatio, 0, MidpointRounding.AwayFromZero);
                    var defaultTargetRow = targetRow;

                    foreach (var reservation in group)
                    {
                        List<Seat> seatsForReservation = [];
                        for (var i = 0; i < reservation.Seats.Count; i++)
                        {
                            if (availableSeats.Select(s => s.Key.Row == targetRow).Count() == 1 && reservation.Seats.Count != 1)
                            {
                                if (targetRow >= defaultTargetRow) targetRow += 1;
                                else targetRow -= 1;
                            }

                            Seat? newReservationSeat = null;
                            while (newReservationSeat == null)
                            {
                                newReservationSeat = availableSeats.FirstOrDefault(s => s.Key.Row == targetRow).Value;
                                if (targetRow >= defaultTargetRow) targetRow += 1;
                                else targetRow -= 1;

                                if (targetRow > newHall.HallDetails.NumberOfSeatsRows)
                                    targetRow = defaultTargetRow - 1;
                                else if (targetRow < newHall.HallDetails.NumberOfSeatsRows)
                                    return Result<Hall>.Failure(HallError.HallIsTooSmall);
                            }                              

                            newReservationSeat.SeatType = reservation.Seats.ElementAt(i).SeatType;
                            seatsForReservation.Add(newReservationSeat);
                            availableSeats.Remove((newReservationSeat.Row, newReservationSeat.Column));
                        }
                        reservationsSeatsDict.Add(reservation, seatsForReservation);
                    }
                }

                foreach (var group in groupedReservations)
                {
                    foreach (var reservation in group)
                    {
                        var seatsForReservation = reservationsSeatsDict.FirstOrDefault(s => s.Key == reservation).Value;
                        reservation.Seats = seatsForReservation;
                        _unitOfWork.GetRepository<Reservation>().Update(reservation);
                    }
                }
            }
           // await _unitOfWork.SaveChangesAsync();
            return Result<Hall>.Success(newHall);
        }

        private async Task<OldEventInfo> GetInfoAboutUpdatedEventProperties(Event oldEventEntity, Event newEventEntity)
        {
            OldEventInfo oldEventInfo = new();

            if (newEventEntity!.Name != oldEventEntity.Name)
            {
                oldEventInfo.Name = oldEventEntity.Name;
                oldEventInfo.SendMailAboutUpdatedEvent = true;
            }

            if (newEventEntity!.Category.Name != oldEventEntity.Category.Name)
            {
                oldEventInfo.CategoryName = oldEventEntity.Category.Name;
            }

            if (newEventEntity.HallId != oldEventEntity.HallId)
            {
                oldEventInfo.HallNr = oldEventEntity.Hall.HallNr;
                _unitOfWork.GetRepository<Hall>().Delete(oldEventEntity.Hall);
                await _unitOfWork.SaveChangesAsync();
                oldEventInfo.SendMailAboutUpdatedEvent = true;
            }

            if (newEventEntity!.StartDate != oldEventEntity.StartDate)
            {
                oldEventInfo.StartDate = oldEventEntity.StartDate;
                oldEventInfo.SendMailAboutUpdatedEvent = true;
            }

            if (newEventEntity!.EndDate != oldEventEntity.EndDate)
            {
                oldEventInfo.EndDate = oldEventEntity.EndDate;
                oldEventInfo.SendMailAboutUpdatedEvent = true;
            }

            return oldEventInfo;
        }

        private void AddTicketsForEvent(ICollection<Event_FestivalTicketRequestDto> eventTickets, Event eventEntity)
        {
            ICollection<Ticket> tickets = [];
            foreach (var ticket in eventTickets)
            {
                var eventTicket = new Ticket
                {
                    Price = ticket.Price,
                    TicketTypeId = ticket.TicketTypeId,
                };
                tickets.Add(eventTicket);
            }
            eventEntity.Tickets = tickets;
        }

        private async Task UpdateTicketsForEvent(ICollection<Event_FestivalTicketRequestDto> newEventTickets, Event oldEvent)
        {
            ICollection<Ticket> updatedTickets = [];

            var oldTicketTypesIds = oldEvent.Tickets.Where(t => t.Festival == null).Select(t => t.TicketTypeId).ToList();
            foreach (var newTicket in newEventTickets)
            {
                if (oldTicketTypesIds.Contains(newTicket.TicketTypeId))
                {
                    var oldTicket = oldEvent.Tickets.Where(t =>
                                        t.Festival == null &&
                                        t.TicketTypeId == newTicket.TicketTypeId).First();

                    updatedTickets.Add(oldTicket);
                    if (oldTicket.Price != newTicket.Price)
                    {
                        oldTicket.Price = newTicket.Price;
                        _unitOfWork.GetRepository<Ticket>().Update(oldTicket);
                    }
                }
                else
                {
                    var eventTicket = new Ticket
                    {
                        Price = newTicket.Price,
                        TicketTypeId = newTicket.TicketTypeId,
                        EventId = oldEvent.Id,
                    };
                    await _unitOfWork.GetRepository<Ticket>().AddAsync(eventTicket);
                }
            }
            if (updatedTickets.Count() != oldEvent.Tickets.Count())
            {
                var ticketsToDelete = oldEvent.Tickets.Except(updatedTickets);
                foreach (var ticket in ticketsToDelete)
                {
                    ticket.IsDeleted = true;
                    _unitOfWork.GetRepository<Ticket>().Update(ticket);
                }
            }
        }

        protected sealed override async Task<Error> ValidateEntity(EventRequestDto? requestDto, int? id = null)
        {
            var userResult = await _userService.GetCurrentUser();
            if (!userResult.IsSuccessful)
                return userResult.Error;

            var user = userResult.Value;

            if (!user.IsInRole(Roles.Admin))
                return AuthError.UserDoesNotHavePremissionToResource;

            var baseValidationError = await base.ValidateEntity(requestDto, id);
            if (baseValidationError != Error.None)
                return baseValidationError;

            if(id != null)
            {
                var eventToUpdateId = id ?? -1;
                var eventEntity = await _repository.GetOneAsync(eventToUpdateId);
                if (eventEntity == null)
                    return Error.NotFound;

                if (eventEntity.IsExpired)
                    return EventError.EventIsExpired;

                if (eventEntity.IsCanceled)
                    return EventError.EventIsCanceled;                
            }

            if (!await IsEntityExistInDB<EventCategory>(requestDto!.CategoryId))
                return EventError.CategoryNotFound;

            if (requestDto.EventTickets.Count() != 0)
            {
                foreach(var eventTicket in requestDto.EventTickets)
                {
                    if (!await IsEntityExistInDB<TicketType>(eventTicket.TicketTypeId))
                        return TicketTypeError.NotFound;
                }
                if (requestDto.EventTickets.DistinctBy(et => et.TicketTypeId).Count() != requestDto.EventTickets.Count())
                    return EventError.EventCanNotHaveManySameTicketTypes;
            }

            var hallEntity = _unitOfWork.GetRepository<Hall>()
                                .GetAllAsync(q => q.Where(h =>
                                h.Id == requestDto.HallId &&
                                h.IsVisible));

            if (hallEntity == null)
                return EventError.HallNotFound;

            if (await _collisionChecker.CheckTimeCollisionsWithEvents(requestDto))
                return EventError.CollisionWithExistingEvent;

            if (await _collisionChecker.CheckTimeCollisionsWithHallRents(requestDto))
                return EventError.CollisionWithExistingHallRent;

            return Error.None;
        }

        protected sealed override Event MapAsEntity(EventRequestDto requestDto)
        {
            var eventEntity = base.MapAsEntity(requestDto);
            eventEntity.Duration = eventEntity.EndDate - eventEntity.StartDate;

            AddEventDetails(eventEntity, requestDto.LongDescription);
            return eventEntity;
        }

        protected sealed override IEntity MapToEntity(EventRequestDto requestDto, Event oldEntity)
        {
            var eventEntity = (Event)base.MapToEntity(requestDto, oldEntity);
            eventEntity.Duration = eventEntity.EndDate - eventEntity.StartDate;
            AddEventDetails(eventEntity, requestDto.LongDescription);
            return eventEntity;
        }

        private static void AddEventDetails(Event eventEntity, string? details)
        {
            if (details != null && details?.Trim() != string.Empty)
            {
                eventEntity.Details = new EventDetails { LongDescription = details };
            }
        }

        protected sealed override IEnumerable<EventResponseDto> MapAsDto(IEnumerable<Event> records)
        {
            return records.Select(entity =>
            {
                var responseDto = entity.AsDto<EventResponseDto>();
                responseDto.Category = entity.Category?.AsDto<EventCategoryResponseDto>();
                responseDto.Details = entity.Details?.AsDto<EventDetailsResponseDto>();
                responseDto.Hall = entity.Hall?.AsDto<HallResponseDto>();
                responseDto.Hall!.Seats = [];
                responseDto.Hall!.Type = null;
                responseDto.Hall!.HallDetails = null;
                return responseDto;
            });
        }


        protected sealed override EventResponseDto MapAsDto(Event entity)
        {
            var responseDto = entity.AsDto<EventResponseDto>();
            responseDto.Category = entity.Category?.AsDto<EventCategoryResponseDto>();
            responseDto.Details = entity.Details?.AsDto<EventDetailsResponseDto>();
            responseDto.Hall = entity.Hall?.AsDto<HallResponseDto>();
            responseDto.Hall!.Seats = [];
            responseDto.Hall!.Type = null;
            responseDto.Hall!.HallDetails = null;
            return responseDto;
        }

        protected async sealed override Task<bool> IsSameEntityExistInDatabase(EventRequestDto entityDto, int? id = null)
        {
            return (await _repository.GetAllAsync(q =>
                      q.Where(entity =>
                        entity.Id != id &&                    
                        entity.Name == entityDto.Name &&
                        entity.EndDate >= DateTime.Now &&
                        !entity.IsCanceled
                      ))).Any();
        }
    }
}
