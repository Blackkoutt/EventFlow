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
using EventFlowAPI.Logic.Identity.Helpers;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces;
using EventFlowAPI.Logic.Helpers.Enums;

namespace EventFlowAPI.Logic.Services.CRUDServices.Services
{
    public sealed class EventService(
        IUnitOfWork unitOfWork,
        IReservationService reservationService,
        IUserService userService,
        IEmailSenderService emailSender,
        ICollisionCheckerService collisionChecker,
        ITicketService ticketService,
        IFileService fileService,
        ICopyMakerService copyMaker
        ) :
        GenericService<
            Event,
            EventRequestDto,
            EventResponseDto
            >(unitOfWork),
        IEventService
    {
        private readonly IUserService _userService = userService;
        private readonly IEmailSenderService _emailSender = emailSender;
        private readonly IReservationService _reservationService = reservationService;
        private readonly ICollisionCheckerService _collisionChecker = collisionChecker;
        private readonly ITicketService _ticketService = ticketService;
        private readonly IFileService _fileService = fileService;
        private readonly ICopyMakerService _copyMaker = copyMaker;


        // do someghing 
        private async Task<ICollection<Festival>> CancelFestivalIfEssential(IEnumerable<Event> eventsToDelete)
        {
            var _festivalRepository = _unitOfWork.GetRepository<Festival>();
            var festivalsToDelete = await _festivalRepository.GetAllAsync(q =>
                                        q.Where(f =>
                                        !f.IsCanceled &&
                                        f.EndDate > DateTime.Now &&
                                        f.Events.Any(e => eventsToDelete.Contains(e)) &&
                                        f.Events.Count(e => !eventsToDelete.Contains(e)) <= 2));

            ICollection<Festival> deletedFestivals = [];

            foreach (var festival in festivalsToDelete)
            {
                deletedFestivals.Add(festival);
                festival.CancelDate = DateTime.Now;
                festival.IsCanceled = true;
                _festivalRepository.Update(festival);
            }

            return deletedFestivals;
        }


        public sealed override async Task<Result<IEnumerable<EventResponseDto>>> GetAllAsync(QueryObject query)
        {
            var eventQuery = query as EventQuery;
            if (eventQuery == null)
                return Result<IEnumerable<EventResponseDto>>.Failure(QueryError.BadQueryObject);

            var records = await _repository.GetAllAsync(q =>
                                                q.ByStatus(eventQuery.Status)
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
            var hallCopyResult = await _copyMaker.MakeCopyOfHall(requestDto!.HallId);
            if (!hallCopyResult.IsSuccessful)
                return Result<EventResponseDto>.Failure(hallCopyResult.Error);

            eventEntity.HallId = hallCopyResult.Value.Id;

            // HallView PDF
            var hallViewFileNameResult = await _fileService.CreateHallViewPDF(eventEntity.Hall, null, eventEntity);
            if (!hallViewFileNameResult.IsSuccessful)
                return Result<EventResponseDto>.Failure(hallViewFileNameResult.Error);
            var hallViewPDFFileName = hallViewFileNameResult.Value;
            eventEntity.Hall.HallViewFileName = hallViewPDFFileName;

            var tickets = _ticketService.GetEventTickets(requestDto.EventTickets);
            eventEntity.Tickets = tickets;

            await _repository.AddAsync(eventEntity);
            await _unitOfWork.SaveChangesAsync();

            var response = MapAsDto(eventEntity);

            return Result<EventResponseDto>.Success(response);
        }

        public async Task<Result<(IEnumerable<Reservation> ReservationsForEvent, Event NewEvent, Event OldEvent)>> UpdateEvent(int id, EventRequestDto? requestDto)
        {
            var validationError = await ValidateEntity(requestDto, id);
            if (validationError != Error.None)
                return Result<(IEnumerable<Reservation>, Event, Event)>.Failure(validationError);

            var eventEntity = await _repository.GetOneAsync(id);
            if (eventEntity == null)
                return Result<(IEnumerable<Reservation>, Event, Event)>.Failure(Error.NotFound);

            var reservationsForEvent = await _reservationService.GetActiveReservationsForEvent(eventEntity.Id);

            var oldEventEntity = new Event
            {
                Name = eventEntity.Name,
                StartDate = eventEntity.StartDate,
                EndDate = eventEntity.EndDate,
                Hall = eventEntity.Hall,
                Category = eventEntity.Category,
                Tickets = eventEntity.Tickets
            };
            var oldHall = await _unitOfWork.GetRepository<Hall>().GetOneAsync(eventEntity.Hall.Id);
            oldEventEntity.Hall.HallDetails = oldHall!.HallDetails;

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

                // validate festival length
                var updateFestivalError  = await UpdateStartAndEndDateOfFestival(
                                    eventId: eventEntity.Id,
                                    startDate: newEventEntity.StartDate,
                                    endDate: newEventEntity.EndDate);
                if (updateFestivalError != Error.None)
                    return Result<(IEnumerable<Reservation>, Event, Event)>.Failure(updateFestivalError);
            }

            if (newEventEntity.HallId != oldEventEntity.HallId)
            {
                var newHallResult = await GetNewEventHallAndUpdateReservationSeats(
                                        newHallId: requestDto!.HallId,
                                        oldHall: oldEventEntity.Hall,
                                        reservationsForEvent: reservationsForEvent);
                if (!newHallResult.IsSuccessful)
                    return Result<(IEnumerable<Reservation>, Event, Event)>.Failure(newHallResult.Error);

                newEventEntity.HallId = newHallResult.Value.Id;
            }

            await _ticketService.UpdateTicketsForEvent(requestDto!.EventTickets, oldEventEntity);

            _repository.Update(newEventEntity);
            return Result<(IEnumerable<Reservation>, Event, Event)>.Success((reservationsForEvent, newEventEntity, oldEventEntity));
        }

        public sealed override async Task<Result<EventResponseDto>> UpdateAsync(int id, EventRequestDto? requestDto)
        {
            var updateEventResult = await UpdateEvent(id, requestDto);
            if (!updateEventResult.IsSuccessful)
                return Result<EventResponseDto>.Failure(updateEventResult.Error);

            var reservationsForEvent = updateEventResult.Value.ReservationsForEvent;
            var newEventEntity = updateEventResult.Value.NewEvent;
            var oldEventEntity = updateEventResult.Value.OldEvent;

            await _unitOfWork.SaveChangesAsync();

            if (reservationsForEvent.Any())
            {             
                if(newEventEntity.HallId != oldEventEntity.HallId)
                {
                    _unitOfWork.GetRepository<Hall>().Delete(oldEventEntity.Hall);
                    await _unitOfWork.SaveChangesAsync();
                }

                if (newEventEntity!.Name != oldEventEntity.Name ||
                    newEventEntity.HallId != oldEventEntity.HallId ||
                    newEventEntity!.StartDate != oldEventEntity.StartDate ||
                    newEventEntity!.EndDate != oldEventEntity.EndDate)
                {
                    var resUpdateError = await _reservationService.SendMailsAboutUpdatedReservations(reservationsForEvent, oldEventEntity, newEventEntity);
                    if (resUpdateError != Error.None)
                        return Result<EventResponseDto>.Failure(resUpdateError);
                }             
            }
            var newEventEntityDto = MapAsDto(newEventEntity);

            return Result<EventResponseDto>.Success(newEventEntityDto);
        }


        public sealed override async Task<Result<object>> DeleteAsync(int id)
        {
            var validationResult = await ValidateBeforeDelete(id);
            if (!validationResult.IsSuccessful)
                return Result<object>.Failure(validationResult.Error);

            var eventEntity = validationResult.Value;

            var reservationsForEvent = await _reservationService.GetActiveReservationsForEvent(eventEntity.Id);

            var festivalsToDelete = await CancelFestivalIfEssential(new List<Event> { eventEntity });
            await _ticketService.DeleteTickets(new List<Event> { eventEntity }, festivalsToDelete);
            await _unitOfWork.SaveChangesAsync();
            
            if (reservationsForEvent.Any())
            {
                var cancelReservationError = await _reservationService.CancelReservationsInCauseOfDeleteEventOrHallOrFestival(reservationsForEvent, eventEntity);
                if (cancelReservationError != Error.None)
                    return Result<object>.Failure(cancelReservationError);
            }
            else
            {
                foreach (var seat in eventEntity.Hall.Seats)
                {
                    _unitOfWork.GetRepository<Seat>().Delete(seat);
                }
            }

            await _fileService.DeleteFile(eventEntity, FileType.PDF, BlobContainer.HallViewsPDF);
            eventEntity.Hall.HallViewFileName = null;
            eventEntity.CancelDate = DateTime.Now;
            eventEntity.IsCanceled = true;
            _repository.Update(eventEntity);

            await _unitOfWork.SaveChangesAsync();

            return Result<object>.Success();
        }



        private void UpdateStartAndEndDateOfReservations(IEnumerable<Reservation> reservationsForEvent, DateTime startDate, DateTime endDate)
        {
            if (reservationsForEvent.Any())
            {
                foreach (var reservation in reservationsForEvent)
                {
                    reservation.StartDate = startDate;
                    reservation.EndDate = endDate;
                    _unitOfWork.GetRepository<Reservation>().Update(reservation);
                }
            }
        }

        private async Task<Error> UpdateStartAndEndDateOfFestival(int eventId, DateTime startDate, DateTime endDate)
        {
            var eventFestivals = await _unitOfWork.GetRepository<Festival>()
                                        .GetAllAsync(q =>
                                        q.Where(f =>
                                        f.Events.Any(e =>
                                        e.Id == eventId)).OrderBy(f => f.StartDate));

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
                        if (festival.Duration > TimeSpan.FromDays(14))
                            return FestivalError.FestivalIsTooLong;
                        _unitOfWork.GetRepository<Festival>().Update(festival);
                    }
                }
            }
            return Error.None;
        }

        private async Task<Result<Hall>> GetNewEventHallAndUpdateReservationSeats(int newHallId, Hall oldHall, IEnumerable<Reservation> reservationsForEvent)
        {
            var hallCopyResult = await _copyMaker.MakeCopyOfHall(newHallId);
            if (!hallCopyResult.IsSuccessful)
                return Result<Hall>.Failure(hallCopyResult.Error);

            var newHall = hallCopyResult.Value;

            if (reservationsForEvent.Count() > newHall.Seats.Count)
                return Result<Hall>.Failure(HallError.HallIsTooSmall);

            if (reservationsForEvent.Any())
            {
                var newHallSeatsGridRows = newHall.HallDetails!.MaxNumberOfSeatsRows;
                var oldHallSeatsGridRows = oldHall.HallDetails!.MaxNumberOfSeatsRows;
                var rowGridRatio = (double)newHallSeatsGridRows / oldHallSeatsGridRows;

                var groupedReservations = reservationsForEvent.GroupBy(r => r.UserId);
                var availableSeats = newHall.Seats
                                        .OrderBy(s => s.GridRow)
                                        .ThenBy(s => s.GridColumn)
                                        .ToDictionary(s => (s.GridRow, s.GridColumn), s => s);


                Dictionary<Reservation, List<Seat>> reservationsSeatsDict = [];
                foreach (var group in groupedReservations)
                {
                    var firstSeat = group.First().Seats.First();
                    int targetGridRow = (int)Math.Round(firstSeat.GridRow * rowGridRatio, 0, MidpointRounding.AwayFromZero);
                    var defaultTargetGridRow = targetGridRow;

                    foreach (var reservation in group)
                    {
                        List<Seat> seatsForReservation = [];
                        for (var i = 0; i < reservation.Seats.Count; i++)
                        {
                            Seat? newReservationSeat = null;
                            int jump = 0;
                            bool ommitFirstTargetRow = false;
                            if (availableSeats.Select(s => s.Key.GridRow == targetGridRow).Count() == 1 && i < reservation.Seats.Count - 1)
                            {
                                ommitFirstTargetRow = true;
                            }

                            for (var j = 0; newReservationSeat == null; j++)
                            {
                                if (!ommitFirstTargetRow)
                                    newReservationSeat = availableSeats.FirstOrDefault(s => s.Key.GridRow == targetGridRow).Value;

                                ommitFirstTargetRow = false;

                                var up = defaultTargetGridRow + jump + 1;
                                var down = defaultTargetGridRow - jump - 1;
                                if (up > newHallSeatsGridRows && down < 0)
                                    return Result<Hall>.Failure(HallError.HallIsTooSmall);

                                if (j % 2 == 0) targetGridRow = defaultTargetGridRow - jump - 1;
                                else
                                {
                                    targetGridRow = defaultTargetGridRow + jump + 1;
                                    jump++;
                                }
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
            return Result<Hall>.Success(newHall);
        }     

        private async Task<Result<Event>> ValidateBeforeDelete(int id)
        {
            if (id < 0)
                return Result<Event>.Failure(Error.RouteParamOutOfRange);

            var eventEntity = await _repository.GetOneAsync(id);
            if (eventEntity == null)
                return Result<Event>.Failure(Error.NotFound);

            if (eventEntity.IsCanceled)
                return Result<Event>.Failure(EventError.EventIsCanceled);

            if (eventEntity.IsExpired)
                return Result<Event>.Failure(EventError.EventIsExpired);

            var userResult = await _userService.GetCurrentUser();
            if (!userResult.IsSuccessful)
                return Result<Event>.Failure(userResult.Error);

            var user = userResult.Value;
            if (!user.IsInRole(Roles.Admin))
                return Result<Event>.Failure(AuthError.UserDoesNotHavePremissionToResource);

            return Result<Event>.Success(eventEntity);
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

            if (id != null)
            {
                var eventEntity = await _repository.GetOneAsync((int)id);
                if (eventEntity == null)
                    return Error.NotFound;

                if (eventEntity.IsExpired)
                    return EventError.EventIsExpired;

                if (eventEntity.IsCanceled)
                    return EventError.EventIsCanceled;
            }

            if (!await IsEntityExistInDB<EventCategory>(requestDto!.CategoryId))
                return EventError.CategoryNotFound;

            var ticketTypeIds = requestDto!.EventTickets.Select(et => et.TicketTypeId);
            if (ticketTypeIds.Count() != ticketTypeIds.Distinct().Count())
                return TicketTypeError.TicketDuplicates;
            var allTicketTypes = await _unitOfWork.GetRepository<TicketType>().GetAllAsync();
            var allTicketTypeIds = allTicketTypes.Select(tt => tt.Id);
            if (!ticketTypeIds.All(id => allTicketTypeIds.Contains(id)))
                return TicketTypeError.NotFound;
            var normalTicketTypeExist = ticketTypeIds.Any(typeId =>
                                    allTicketTypes.Any(tt =>
                                    tt.Name.Equals("Bilet normalny", StringComparison.OrdinalIgnoreCase) &&
                                    tt.Id == typeId));
            if (!normalTicketTypeExist)
                return TicketTypeError.NormalTicketTypeNotFound;

            var hallEntity = await _unitOfWork.GetRepository<Hall>()
                                    .GetAllAsync(q => q.Where(h =>
                                    h.Id == requestDto.HallId &&
                                    h.IsVisible));

            if (hallEntity == null)
                return EventError.HallNotFound;

            if (await _collisionChecker.CheckTimeCollisionsWithEvents(requestDto, id))
                return EventError.CollisionWithExistingEvent;

            if (await _collisionChecker.CheckTimeCollisionsWithHallRents(requestDto))
                return EventError.CollisionWithExistingHallRent;

            return Error.None;
        }

        protected sealed override Event MapAsEntity(EventRequestDto requestDto)
        {
            var eventEntity = base.MapAsEntity(requestDto);
            eventEntity.Duration = eventEntity.EndDate - eventEntity.StartDate;
            AddOrUpdateEventDetails(eventEntity, requestDto.LongDescription);
            return eventEntity;
        }

        protected sealed override IEntity MapToEntity(EventRequestDto requestDto, Event oldEntity)
        {

            var eventEntity = (Event)base.MapToEntity(requestDto, oldEntity);
            eventEntity.Duration = eventEntity.EndDate - eventEntity.StartDate;
            AddOrUpdateEventDetails(eventEntity, requestDto.LongDescription);
            return eventEntity;
        }

        private void AddOrUpdateEventDetails(Event eventEntity, string? longDescription)
        {
            if (!string.IsNullOrEmpty(longDescription))
            {
                if (eventEntity.Details == null)
                {
                    eventEntity.Details = new EventDetails
                    {
                        LongDescription = longDescription
                    };
                }
                else
                {
                    eventEntity.Details!.LongDescription = longDescription;
                }
            }
        }

        protected sealed override IEnumerable<EventResponseDto> MapAsDto(IEnumerable<Event> records)
        {
            return records.Select(entity =>
            {
                var responseDto = entity.AsDto<EventResponseDto>();
                responseDto.Category = entity.Category?.AsDto<EventCategoryResponseDto>();
                responseDto.Details = entity.Details?.AsDto<EventDetailsResponseDto>();
                responseDto.Tickets = [];
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
            responseDto.Tickets = entity.Tickets.Select(t =>
            {
                var ticket = t.AsDto<TicketResponseDto>();
                ticket.Event = null;
                ticket.Festival = null;
                return ticket;
            }).ToList();
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
