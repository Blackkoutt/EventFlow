using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Extensions;
using EventFlowAPI.Logic.Identity.Helpers;
using EventFlowAPI.Logic.Mapper.Extensions;
using EventFlowAPI.Logic.Query;
using EventFlowAPI.Logic.Query.Abstract;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces;
using EventFlowAPI.Logic.Services.CRUDServices.Services.BaseServices;
using EventFlowAPI.Logic.UnitOfWork;
using System.Collections.Immutable;

namespace EventFlowAPI.Logic.Services.CRUDServices.Services
{
    public sealed class FestivalService(
        IUnitOfWork unitOfWork,
        ITicketService ticketService,
        IUserService userService,
        IEventService eventService,
        IReservationService reservationService) :
        GenericService<
            Festival,
            FestivalRequestDto,
            FestivalResponseDto
        >(unitOfWork),
        IFestivalService
    {
        private readonly IUserService _userService = userService;
        private readonly ITicketService _ticketService = ticketService;
        private readonly IEventService _eventService = eventService;
        private readonly IReservationService _reservationService = reservationService;
        public sealed override async Task<Result<IEnumerable<FestivalResponseDto>>> GetAllAsync(QueryObject query)
        {
            var festivalQuery = query as FestivalQuery;
            if (festivalQuery == null)
                return Result<IEnumerable<FestivalResponseDto>>.Failure(QueryError.BadQueryObject);

            var records = await _repository.GetAllAsync(q =>
                                                q.ByStatus(festivalQuery.Status)
                                                .SortBy(festivalQuery.SortBy, festivalQuery.SortDirection));

            var response = MapAsDto(records);

            return Result<IEnumerable<FestivalResponseDto>>.Success(response);
        }

        public sealed override async Task<Result<FestivalResponseDto>> AddAsync(FestivalRequestDto? requestDto)
        {          
            var validationError = await ValidateEntity(requestDto);
            if (validationError != Error.None)
                return Result<FestivalResponseDto>.Failure(validationError);

            var festivalEntity = await CreateFestivalEntity(requestDto!);

            var tickets = _ticketService.GetFestivalTickets(requestDto!.FestivalTickets, festivalEntity.Events);
            festivalEntity.Tickets = tickets;

            await _repository.AddAsync(festivalEntity);
            await _unitOfWork.SaveChangesAsync();

            var response = MapAsDto(festivalEntity);

            return Result<FestivalResponseDto>.Success(response);
        }

        public sealed override async Task<Result<FestivalResponseDto>> UpdateAsync(int id, FestivalRequestDto? requestDto)
        {
            var validationError = await ValidateEntity(requestDto, id);
            if (validationError != Error.None)
                return Result<FestivalResponseDto>.Failure(validationError);

            var festival = await _repository.GetOneAsync(id);
            if (festival == null)
                return Result<FestivalResponseDto>.Failure(Error.NotFound);

            bool needToSendMail = false;

            var oldFestival = new Festival
            {
                Name = festival.Name,
                StartDate = festival.StartDate,
                EndDate = festival.EndDate,
                Events = festival.Events.ToList(),
            };

            if(festival.Name != requestDto!.Name)
            {
                festival.Name = requestDto!.Name;
                needToSendMail = true;  
            }

            if(festival.ShortDescription != requestDto!.ShortDescription)
                festival.ShortDescription = requestDto!.ShortDescription; 
            
            if(festival.Details != null && festival.Details.LongDescription != requestDto?.Details?.LongDescription)
                festival.Details.LongDescription = requestDto?.Details?.LongDescription;

            // Events
            // jesli zmieni sie nazwa festiwalu lub dodane zostanie wydarzenie lub usuniete z festiwalu to poinformuj uzytkownika
            // w event service - jesli zmieni sie data wydarzenia i wchodzi ono w sklad jakiekolwiek festiwalu to zmien daty rozpoczenia festiali  
            List<Event> newFestivalEventList = [];
            List<Reservation> reservationsForEvents = [];
            foreach(var (eventId, updateEventRequestData) in requestDto!.Events)
            {
                var eventEntity = await _unitOfWork.GetRepository<Event>().GetOneAsync(eventId);  
                if(updateEventRequestData != null)
                {
                    var updateRequest = new EventRequestDto();
                    if (updateEventRequestData.Name != null && eventEntity!.Name != updateEventRequestData.Name)
                    {
                        updateRequest.Name = updateEventRequestData.Name;
                        needToSendMail = true;
                    }
                    else updateRequest.Name = eventEntity!.Name;

                    if(updateEventRequestData.StartDate != null && eventEntity!.StartDate != updateEventRequestData.StartDate)
                    {
                        updateRequest.StartDate = (DateTime)updateEventRequestData.StartDate;
                        needToSendMail = true;
                    }
                    else updateRequest.StartDate = eventEntity!.StartDate;

                    if (updateEventRequestData.EndDate != null && eventEntity!.EndDate != updateEventRequestData.EndDate)
                    {
                        updateRequest.EndDate = (DateTime)updateEventRequestData.EndDate;
                        needToSendMail = true;
                    }
                    else updateRequest.EndDate = eventEntity!.EndDate;

                    if (updateEventRequestData.ShortDescription != null && eventEntity!.ShortDescription != updateEventRequestData.ShortDescription)
                        updateRequest.ShortDescription = updateEventRequestData.ShortDescription;
                    else updateRequest.ShortDescription = eventEntity!.ShortDescription;

                    if (updateEventRequestData.LongDescription != null && eventEntity!.Details?.LongDescription != updateEventRequestData.LongDescription)
                        updateRequest.LongDescription = updateEventRequestData.LongDescription;
                    else updateRequest.LongDescription = eventEntity!.Details?.LongDescription;

                    if (updateEventRequestData.CategoryId != null && eventEntity!.CategoryId != updateEventRequestData.CategoryId)
                        updateRequest.CategoryId = (int)updateEventRequestData.CategoryId;
                    else updateRequest.CategoryId = eventEntity!.CategoryId;

                    if (updateEventRequestData.HallId != null && eventEntity!.HallId != updateEventRequestData.HallId)
                    {
                        updateRequest.HallId = (int)updateEventRequestData.HallId;
                        needToSendMail = true;
                    }
                    else updateRequest.HallId = eventEntity!.HallId;

                    List<Event_FestivalTicketRequestDto> ticketsDto = [];
                    foreach(var ticket in eventEntity!.Tickets)
                    {
                        ticketsDto.Add(new Event_FestivalTicketRequestDto
                        {
                            Price = ticket.Price,
                            TicketTypeId = ticket.TicketTypeId
                        });
                    }
                    updateRequest.EventTickets = ticketsDto;

                    var updateEventResult = await _eventService.UpdateEvent(eventId, updateRequest);
                    if (!updateEventResult.IsSuccessful)
                        return Result<FestivalResponseDto>.Failure(updateEventResult.Error);

                    eventEntity = updateEventResult.Value.NewEvent;
                    var reservationsForEvent = updateEventResult.Value.ReservationsForEvent;
                    var oldEvent = updateEventResult.Value.OldEvent;

                    reservationsForEvents.AddRange(reservationsForEvent);
                }
                newFestivalEventList.Add(eventEntity!);
            }
            festival.Events = newFestivalEventList;

            // Media Patrons
            var newFestivalMediaPatrons = (await _unitOfWork.GetRepository<MediaPatron>().GetAllAsync(q =>
                                                q.Where(mp => requestDto.MediaPatronIds.Contains(mp.Id)))).ToList();
            festival.MediaPatrons = newFestivalMediaPatrons;

            // Organizers
            var newFestivalOrganizers = (await _unitOfWork.GetRepository<Organizer>().GetAllAsync(q =>
                                                q.Where(o => requestDto.OrganizerIds.Contains(o.Id)))).ToList();
            festival.Organizers = newFestivalOrganizers;

            // Sponsors
            var newFestivalSponsors = (await _unitOfWork.GetRepository<Sponsor>().GetAllAsync(q =>
                                                q.Where(s => requestDto.SponsorIds.Contains(s.Id)))).ToList();
            festival.Sponsors = newFestivalSponsors;

            // Tickets
            await _ticketService.UpdateTicketsForFestival(requestDto!.FestivalTickets, festival);
            _repository.Update(festival);
            await _unitOfWork.SaveChangesAsync();

            if (needToSendMail)
            {
                var sendError = await _reservationService.SendMailsAboutUpdatedReservations(reservationsForEvents, oldFestival, festival);
                if (sendError != Error.None)
                    return Result<FestivalResponseDto>.Failure(sendError);
            }

            var newFestivalEntityDto = MapAsDto(festival);

            return Result<FestivalResponseDto>.Success(newFestivalEntityDto);
        }


        public sealed override async Task<Result<object>> DeleteAsync(int id)
        {
            var validationResult = await ValidateBeforeDelete(id);
            if (!validationResult.IsSuccessful)
                return Result<object>.Failure(validationResult.Error);

            var festival = validationResult.Value;
            var reservationsForFestival = await _reservationService.GetActiveReservationsForFestival(festival.Id);

            await _ticketService.DeleteTickets([], new List<Festival> { festival });
            await _unitOfWork.SaveChangesAsync();

            festival.CancelDate = DateTime.Now;
            festival.IsCanceled = true;
            _repository.Update(festival);

            if (reservationsForFestival.Any())
            {
                var cancelReservationError = await _reservationService.CancelReservationsInCauseOfDeleteEventOrHallOrFestival(reservationsForFestival, null, festival);
                if (cancelReservationError != Error.None)
                    return Result<object>.Failure(cancelReservationError);
            }

            await _unitOfWork.SaveChangesAsync();

            return Result<object>.Success();
        }


        // idk czy dodac cos pozatym to powtarza sie i dobrze by bylo moze dac to do klasy bazowej czy cos
        private async Task<Result<Festival>> ValidateBeforeDelete(int id)
        {
            if (id < 0)
                return Result<Festival>.Failure(Error.RouteParamOutOfRange);

            var festival = await _repository.GetOneAsync(id);
            if (festival == null)
                return Result<Festival>.Failure(Error.NotFound);

            if (festival.IsCanceled)
                return Result<Festival>.Failure(FestivalError.FestivalIsCanceled);

            if (festival.IsExpired)
                return Result<Festival>.Failure(FestivalError.FestivalIsExpired);

            var userResult = await _userService.GetCurrentUser();
            if (!userResult.IsSuccessful)
                return Result<Festival>.Failure(userResult.Error);

            var user = userResult.Value;
            if (!user.IsInRole(Roles.Admin))
                return Result<Festival>.Failure(AuthError.UserDoesNotHavePremissionToResource);

            return Result<Festival>.Success(festival);
        }



        private async Task<Festival> CreateFestivalEntity(FestivalRequestDto requestDto)
        {
            var festivalEvents = (await _unitOfWork.GetRepository<Event>().GetAllAsync(q =>
                                        q.Where(f => requestDto!.Events.ContainsKey(f.Id)).OrderBy(f => f.StartDate))).ToList();

            var festivalMediaPatrons = (await _unitOfWork.GetRepository<MediaPatron>().GetAllAsync(q =>
                                                q.Where(mp => requestDto.MediaPatronIds.Contains(mp.Id)))).ToList();

            var allOrganizers = await _unitOfWork.GetRepository<Organizer>().GetAllAsync();
            var festivalOrganizers = allOrganizers.Where(o => requestDto.OrganizerIds.Contains(o.Id)).ToList();
            if (!festivalOrganizers.Any(o => o.Name.Equals("EventFlow", StringComparison.OrdinalIgnoreCase)))
            {
                var eventFlowOrganizer = allOrganizers.FirstOrDefault(o => o.Name.Equals("EventFlow", StringComparison.OrdinalIgnoreCase));
                if (eventFlowOrganizer == null)
                {
                    eventFlowOrganizer = new Organizer { Name = "EventFlow" };
                }
                festivalOrganizers.Add(eventFlowOrganizer);
            }

            var festivalSponsors = (await _unitOfWork.GetRepository<Sponsor>().GetAllAsync(q =>
                                    q.Where(s => requestDto.SponsorIds.Contains(s.Id)))).ToList();

            var festivalStartDate = festivalEvents.First().StartDate;
            var festivalEndDate = festivalEvents.Last().EndDate;
            var festivalDetails = string.IsNullOrEmpty(requestDto.Details?.LongDescription) ? null : new FestivalDetails { LongDescription = requestDto.Details?.LongDescription };

            return new Festival
            {
                Name = requestDto.Name,
                ShortDescription = requestDto.ShortDescription,
                AddDate = DateTime.Now,
                StartDate = festivalStartDate,
                EndDate = festivalEndDate,
                Duration = festivalEndDate - festivalStartDate,
                CancelDate = null,
                IsCanceled = false,
                Details = festivalDetails,
                Events = festivalEvents,
                MediaPatrons = festivalMediaPatrons,
                Organizers = festivalOrganizers,
                Sponsors = festivalSponsors,
            };
        }

        protected sealed override async Task<Error> ValidateEntity(FestivalRequestDto? requestDto, int? id = null)
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
                var festival = await _repository.GetOneAsync((int)id);
                if (festival == null)
                    return Error.NotFound;

                if (festival.IsExpired)
                    return FestivalError.FestivalIsExpired;

                if (festival.IsCanceled)
                    return FestivalError.FestivalIsCanceled;
            }

            // Events
            if (requestDto!.Events.Count != requestDto!.Events.Distinct().Count())
                return FestivalError.EventDuplicates;
            if (requestDto!.Events.Count() < 2)
                return FestivalError.TooFewEventsInFestival;
            if (requestDto!.Events.Count > 12)
                return FestivalError.TooMuchEventsInFestival;
            var allEventsIdsInDB = (await _unitOfWork.GetRepository<Event>().GetAllAsync()).Select(f => f.Id);
            if (!requestDto!.Events.Keys.All(id => allEventsIdsInDB.Contains(id)))
                return EventError.EventNotFound;

            var festivalEvents = (await _unitOfWork.GetRepository<Event>().GetAllAsync(q =>
                                        q.Where(f => requestDto!.Events.ContainsKey(f.Id)).OrderBy(f => f.StartDate)));

            if(festivalEvents.Last().EndDate - festivalEvents.First().StartDate > TimeSpan.FromDays(14))
                return FestivalError.FestivalIsTooLong;

            var festivalEventsCount = festivalEvents.Count();
            for (var i = 0; i < festivalEventsCount; i++)
            {
                var eventEntity = festivalEvents.ElementAt(i);
                if(eventEntity.IsCanceled)
                    return EventError.EventIsCanceled;
                if (eventEntity.IsExpired)
                    return EventError.EventIsExpired;

                if(i + 1 < festivalEventsCount)
                {
                    var nextEventEntity = festivalEvents.ElementAt(i + 1);
                    if(nextEventEntity.StartDate - eventEntity.StartDate > TimeSpan.FromHours(48))
                        return FestivalError.TooMuchTimeBetweenEvents;
                }
                if(eventEntity.Festivals.Count() >= 3)
                    return EventError.EventIsPartOfTooMuchFestivals;
            }

            // Tickets
            var ticketTypeIds = requestDto!.FestivalTickets.Select(ft => ft.TicketTypeId);
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

            // Media Patrons 
            if (requestDto!.MediaPatronIds.Count != requestDto!.MediaPatronIds.Distinct().Count())
                return FestivalError.MediaPatronDuplicates;
            if (requestDto!.MediaPatronIds.Count > 10)
                return FestivalError.TooMuchMediaPatronsInFestival;
            var allMediaPatronsIdsInDB = (await _unitOfWork.GetRepository<MediaPatron>().GetAllAsync()).Select(mp => mp.Id);
            if (!requestDto!.MediaPatronIds.All(id => allMediaPatronsIdsInDB.Contains(id)))
                return MediaPatronError.MediaPatronNotFound;


            // Organizers if organizer = [] => EventFlow
            if (requestDto!.OrganizerIds.Count != requestDto!.OrganizerIds.Distinct().Count())
                return FestivalError.OrganizerDuplicates;
            if (requestDto!.OrganizerIds.Count > 10)
                return FestivalError.TooMuchOrganizersInFestival;
            var allOrganizersIdsInDB = (await _unitOfWork.GetRepository<Organizer>().GetAllAsync()).Select(o => o.Id);
            if (!requestDto!.OrganizerIds.All(id => allOrganizersIdsInDB.Contains(id)))
                return OrganizerError.OrganizerNotFound;

            // Sponsors
            if (requestDto!.SponsorIds.Count != requestDto!.SponsorIds.Distinct().Count())
                return FestivalError.SponsorDuplicates;
            if (requestDto!.SponsorIds.Count > 10)
                return FestivalError.TooMuchSponsorsInFestival;
            var allSponsorsIdsInDB = (await _unitOfWork.GetRepository<Sponsor>().GetAllAsync()).Select(s => s.Id);
            if (!requestDto!.SponsorIds.All(id => allSponsorsIdsInDB.Contains(id)))
                return SponsorError.SponsorNotFound;

            return Error.None;
        }


        public async Task<ICollection<Festival>> CancelFestivalIfEssential(IEnumerable<Event> eventsToDelete)
        {
            var festivalsToDelete = await _repository.GetAllAsync(q =>
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
                _repository.Update(festival);
            }

            return deletedFestivals;
        }


        protected sealed override IEnumerable<FestivalResponseDto> MapAsDto(IEnumerable<Festival> records)
        {
            return records.Select(entity =>
            {
                var responseDto = entity.AsDto<FestivalResponseDto>();
                responseDto.Details = entity.Details?.AsDto<FestivalDetailsResponseDto>();
                responseDto.Events = entity.Events.Select(e =>
                {
                    var eventEntity = e.AsDto<EventResponseDto>();
                    eventEntity.Category = e.Category.AsDto<EventCategoryResponseDto>();
                    eventEntity.Details = null;
                    eventEntity.Hall = e.Hall.AsDto<HallResponseDto>();
                    eventEntity.Hall.HallDetails = null;
                    eventEntity.Hall.Seats = [];
                    eventEntity.Hall.Type = null;
                    eventEntity.Tickets = [];
                    return eventEntity;
                }).ToList();
                responseDto.MediaPatrons = entity.MediaPatrons.Select(e => e.AsDto<MediaPatronResponseDto>()).ToList();
                responseDto.Organizers = entity.Organizers.Select(e => e.AsDto<OrganizerResponseDto>()).ToList();
                responseDto.Sponsors = entity.Sponsors.Select(e => e.AsDto<SponsorResponseDto>()).ToList();
                return responseDto;
            });
        }


        protected sealed override FestivalResponseDto MapAsDto(Festival entity)
        {
            var responseDto = entity.AsDto<FestivalResponseDto>();
            responseDto.Details = entity.Details?.AsDto<FestivalDetailsResponseDto>();
            responseDto.Events = entity.Events.Select(e =>
            {
                var eventEntity = e.AsDto<EventResponseDto>();
                eventEntity.Category = e.Category.AsDto<EventCategoryResponseDto>();
                eventEntity.Details = null;
                eventEntity.Hall = e.Hall.AsDto<HallResponseDto>();
                eventEntity.Hall.HallDetails = null;
                eventEntity.Hall.Seats = [];
                eventEntity.Hall.Type = null;
                eventEntity.Tickets = [];
                return eventEntity;
            }).ToList();
            responseDto.MediaPatrons = entity.MediaPatrons.Select(e => e.AsDto<MediaPatronResponseDto>()).ToList();
            responseDto.Organizers = entity.Organizers.Select(e => e.AsDto<OrganizerResponseDto>()).ToList();
            responseDto.Sponsors = entity.Sponsors.Select(e => e.AsDto<SponsorResponseDto>()).ToList();
            return responseDto;
        }

        protected async sealed override Task<bool> IsSameEntityExistInDatabase(FestivalRequestDto entityDto, int? id = null)
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
