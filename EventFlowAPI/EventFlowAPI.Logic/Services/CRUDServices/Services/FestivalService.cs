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

namespace EventFlowAPI.Logic.Services.CRUDServices.Services
{
    public sealed class FestivalService(
        IUnitOfWork unitOfWork,
        IUserService userService) :
        GenericService<
            Festival,
            FestivalRequestDto,
            FestivalResponseDto
        >(unitOfWork),
        IFestivalService
    {
        private readonly IUserService _userService = userService;
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

            var festivalEntity = MapAsEntity(requestDto!);

            // data rozpoczenia i zakonczenia festiwalu to data rozpoczecia najblizszego i koncowego zakonczenia
            //eventEntity.Duration = eventEntity.EndDate - eventEntity.StartDate;
            festivalEntity.AddDate = DateTime.Now;

            // przy dodawaniu festiwalu add ticketów festiwalowych
            //AddTicketsForEvent(requestDto.EventTickets, eventEntity);

            await _repository.AddAsync(festivalEntity);
            await _unitOfWork.SaveChangesAsync();

            var response = MapAsDto(festivalEntity);

            return Result<FestivalResponseDto>.Success(response);
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
            if (requestDto!.EventIds.Count != requestDto!.EventIds.Distinct().Count())
                return FestivalError.EventDuplicates;
            if (requestDto!.EventIds.Count() < 2)
                return FestivalError.TooFewEventsInFestival;
            if (requestDto!.EventIds.Count > 12)
                return FestivalError.TooMuchEventsInFestival;
            var allEventsIdsInDB = (await _unitOfWork.GetRepository<Event>().GetAllAsync()).Select(f => f.Id);
            if (!requestDto!.EventIds.All(id => allEventsIdsInDB.Contains(id)))
                return EventError.EventNotFound;

            var festivalEvents = await _unitOfWork.GetRepository<Event>().GetAllAsync(q =>
                                        q.Where(f => requestDto!.EventIds.Contains(f.Id)));

            foreach(var eventEntity in festivalEvents)
            {
                if(eventEntity.IsCanceled)
                    return EventError.EventIsCanceled;
                if (eventEntity.IsExpired)
                    return EventError.EventIsExpired;
                // sprawdz czy wydarzenia nie sa od siebie zbyt odlegle

            }
            // max festival duration
            // sprawdz czy event nie wchodzi w sklad innego festiwlu ?? ewentualnie jesli wchodzi to max ilosc festiwali w sklad ktrocyh moze wchodzic event

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
            var entities = await _repository.GetAllAsync(q =>
                      q.Where(entity =>
                        entity.Name == entityDto.Name &&
                        entity.EndDate >= DateTime.Now
                       ));

            return IsEntityWithOtherIdExistInList(entities, id);
        }
    }
}
