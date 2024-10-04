using EventFlowAPI.DB.Entities;
using EventFlowAPI.DB.Entities.Abstract;
using EventFlowAPI.Logic.DTO.Abstract;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Helpers;
using EventFlowAPI.Logic.Identity.Helpers;
using EventFlowAPI.Logic.Mapper.Extensions;
using EventFlowAPI.Logic.Query;
using EventFlowAPI.Logic.Query.Abstract;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces;
using EventFlowAPI.Logic.Services.CRUDServices.Services.BaseServices;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces;
using EventFlowAPI.Logic.UnitOfWork;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;

namespace EventFlowAPI.Logic.Services.CRUDServices.Services
{
    public sealed class HallService(
        IUnitOfWork unitOfWork,
        IUserService userService,
        ISeatService seatService,
        IReservationService reservationService,
        IEmailSenderService emailSender, 
        IFestivalService festivalService,
        ITicketService ticketService) :
        GenericService<
            Hall,
            HallRequestDto,
            HallResponseDto
        >(unitOfWork),
    IHallService
    {
        private readonly IUserService _userService = userService;
        private readonly ISeatService _seatService = seatService;
        private readonly IReservationService _reservationService = reservationService;
        private readonly IEmailSenderService _emailSender = emailSender;
        private readonly IFestivalService _festivalService = festivalService;
        private readonly ITicketService _ticketService = ticketService;

        public sealed override async Task<Result<IEnumerable<HallResponseDto>>> GetAllAsync(QueryObject query)
        {
            var hallQuery = query as HallQuery;
            if (hallQuery == null)
                return Result<IEnumerable<HallResponseDto>>.Failure(QueryError.BadQueryObject);

            var records = await _repository.GetAllAsync(q => q.Where(entity => entity.IsVisible));
            var response = MapAsDto(records);

            return Result<IEnumerable<HallResponseDto>>.Success(response);
        }


        public sealed override async Task<Result<HallResponseDto>> AddAsync(HallRequestDto? requestDto)
        {
            var validationError = await ValidateEntity(requestDto);
            if (validationError != Error.None)
                return Result<HallResponseDto>.Failure(validationError);

            var hallEntity = MapAsEntity(requestDto!);

            var area = requestDto!.HallDetails.TotalLength * requestDto!.HallDetails.TotalWidth;
            hallEntity.HallDetails!.TotalArea = Math.Round(area, 2);
            await _repository.AddAsync(hallEntity);
            await _unitOfWork.SaveChangesAsync();

            hallEntity.DefaultId = hallEntity.Id;
            _repository.Update(hallEntity);
            await _unitOfWork.SaveChangesAsync();

            var hallEntityDto = MapAsDto(hallEntity);

            return Result<HallResponseDto>.Success(hallEntityDto);
        }


        public sealed override async Task<Result<HallResponseDto>> UpdateAsync(int id, HallRequestDto? requestDto)
        {
            var error = await ValidateEntity(requestDto, id);
            if (error != Error.None)
                return Result<HallResponseDto>.Failure(error);

            var hallEntity = await _repository.GetOneAsync(id);
            if (hallEntity == null)
                return Result<HallResponseDto>.Failure(Error.NotFound);

            if (!hallEntity.IsVisible)
                return Result<HallResponseDto>.Failure(Error.NotFound);

            var allSameHalls = await _repository.GetAllAsync(q =>
                                    q.Where(h => 
                                    h.Id != hallEntity.Id &&
                                    h.DefaultId == hallEntity.DefaultId));


            if (hallEntity.HallNr != requestDto!.HallNr ||
                hallEntity.Floor != requestDto!.Floor ||
                hallEntity.HallDetails!.TotalLength != requestDto!.HallDetails!.TotalLength ||
                hallEntity.HallDetails!.TotalWidth != requestDto!.HallDetails!.TotalWidth)
            {
                foreach (var hall in allSameHalls)
                {
                    hall.HallNr = requestDto!.HallNr;
                    hall.Floor = requestDto!.Floor;
                    hall.HallDetails!.TotalLength = requestDto!.HallDetails.TotalLength;
                    hall.HallDetails!.TotalWidth = requestDto!.HallDetails.TotalWidth;
                    var area = requestDto!.HallDetails.TotalLength * requestDto!.HallDetails.TotalWidth;
                    hall.HallDetails!.TotalArea = Math.Round(area, 2);

                    _repository.Update(hall);
                }
            }

            var allActiveReservations = await _unitOfWork.GetRepository<Reservation>().GetAllAsync(q =>
                                            q.Where(r =>
                                            !r.IsCanceled &&
                                            r.EndOfReservationDate > DateTime.Now &&
                                            r.Ticket.Event.Hall.DefaultId == hallEntity.DefaultId));

            var allActiveHallRents = await _unitOfWork.GetRepository<HallRent>().GetAllAsync(q =>
                                            q.Where(hr =>
                                            hr.Hall.DefaultId == hallEntity.DefaultId &&
                                            !hr.IsCanceled &&
                                            hr.EndDate > DateTime.Now));
            
            // seats i hall type nie ma znaczenia bo update ogolny
            if (allActiveReservations.Any() && hallEntity.HallNr != requestDto!.HallNr)
            {
                OldEventInfo oldEventInfo = new();
                oldEventInfo.HallNr = hallEntity.HallNr;
                await _reservationService.SendMailsAboutUpdatedReservations(allActiveReservations, oldEventInfo);
            }
            
            if (allActiveHallRents.Any() && 
                (hallEntity.HallNr != requestDto!.HallNr ||
                hallEntity.HallDetails!.TotalLength != requestDto!.HallDetails!.TotalLength ||
                hallEntity.HallDetails!.TotalWidth != requestDto!.HallDetails!.TotalWidth))
            {
                // send mail to all active hall rents !!
            }

            // Other changes -> update only this hallEntity, only for new resrvations and hall rents
            var newHallEntity = (Hall)MapToEntity(requestDto, hallEntity);

            _repository.Update(newHallEntity);

            await _unitOfWork.SaveChangesAsync();

            var newHallEntityDto = MapAsDto(newHallEntity);

            return Result<HallResponseDto>.Success(newHallEntityDto);
        }

        

        public async Task<Result<HallResponseDto>> UpdateHallForEvent(int eventId, EventHallRequestDto? requestDto)
        {
            var validationError = await ValidateBeforeUpdateHallForEvent(requestDto, eventId);
            if (validationError != Error.None)
                return Result<HallResponseDto>.Failure(validationError);

            var eventEntity = await _unitOfWork.GetRepository<Event>().GetOneAsync(eventId);
            var hallEntity = eventEntity!.Hall;                    

            hallEntity = (Hall)MapToEntity(requestDto!, hallEntity);

            _repository.Update(hallEntity);
            await _unitOfWork.SaveChangesAsync();

            var hallEntityDto = MapAsDto(hallEntity);

            return Result<HallResponseDto>.Success(hallEntityDto);
        }


        public async Task<Result<HallResponseDto>> UpdateHallForRent(int rentId, HallRent_HallRequestDto? requestDto)
        {
            var validationError = await ValidateBeforeUpdateHallForRent(requestDto, rentId);
            if (validationError != Error.None)
                return Result<HallResponseDto>.Failure(validationError);

            var rentEntity = await _unitOfWork.GetRepository<HallRent>().GetOneAsync(rentId);
            var hallEntity = rentEntity!.Hall;

            hallEntity = (Hall)MapToEntity(requestDto!, hallEntity);

            _repository.Update(hallEntity);
            await _unitOfWork.SaveChangesAsync();

            var hallEntityDto = MapAsDto(hallEntity);

            return Result<HallResponseDto>.Success(hallEntityDto);
        }


        public sealed override async Task<Result<object>> DeleteAsync(int id)
        {
            var validationResult = await ValidateBeforeDelete(id);
            if (!validationResult.IsSuccessful)
                return Result<object>.Failure(validationResult.Error);

            var hall = validationResult.Value;

            var allCopiesOfHall = await _repository.GetAllAsync(q => q.Where(h =>
                                                                    h.Id != hall.Id &&
                                                                    h.DefaultId == hall.Id));

            var allCopiesOfHallId = allCopiesOfHall.Select(h => h.Id).ToList();

            if (allCopiesOfHallId.Any())
            {   
                await CancelHallRentsOnHall(allCopiesOfHallId);

                var reservations = await _unitOfWork.GetRepository<Reservation>()
                                    .GetAllAsync(q =>
                                    q.Where(r =>
                                    !r.IsCanceled &&
                                    r.EndOfReservationDate > DateTime.Now &&
                                    allCopiesOfHallId.Contains(r.Ticket.Event.HallId)));

                var eventsToDelete = await CancelEventsInHall(allCopiesOfHallId);
                var festivalsToDelete = await _festivalService.CancelFestivalIfEssential(eventsToDelete);
                await _ticketService.DeleteTickets(eventsToDelete, festivalsToDelete);
                await _unitOfWork.SaveChangesAsync();

                var cancelReservationError = await _reservationService.CancelReservationsInCauseOfDeleteEventOrHall(reservations);
                if (cancelReservationError != Error.None)
                    return Result<object>.Failure(cancelReservationError);     
            }
            foreach(var hallCopy in allCopiesOfHall)
            {
                hallCopy.DefaultId = null;
                _repository.Update(hallCopy);
            }
            _repository.Delete(hall);
            await _unitOfWork.SaveChangesAsync();

            return Result<object>.Success();
        }

        private async Task<Result<Hall>> ValidateBeforeDelete(int id)
        {
            var userResult = await _userService.GetCurrentUser();
            if (!userResult.IsSuccessful)
                return Result<Hall>.Failure(userResult.Error);

            var user = userResult.Value;
            if (!user.UserRoles.Contains(Roles.Admin))
                return Result<Hall>.Failure(AuthError.UserDoesNotHaveSpecificRole);

            if (id < 0)
                return Result<Hall>.Failure(Error.RouteParamOutOfRange);

            var hall = await _repository.GetOneAsync(id);
            if (hall == null || !hall.IsVisible)
                return Result<Hall>.Failure(Error.NotFound);

            return Result<Hall>.Success(hall);
        }
       

        

        private async Task<ICollection<Event>> CancelEventsInHall(IEnumerable<int> allCopiesOfHallId)
        {
            var eventsToDelete = await _unitOfWork.GetRepository<Event>()
                                .GetAllAsync(q =>
                                q.Where(e =>
                                !e.IsCanceled &&
                                e.EndDate > DateTime.Now &&
                                allCopiesOfHallId.Contains(e.HallId)));

            foreach (var eventEntity in eventsToDelete)
            {
                eventEntity.CancelDate = DateTime.Now;
                eventEntity.IsCanceled = true;
                _unitOfWork.GetRepository<Event>().Update(eventEntity);
            }

            return eventsToDelete.ToList();
        }



        private async Task CancelHallRentsOnHall(List<int> allCopiesOfHallId)
        {
            var hallRentRepo = _unitOfWork.GetRepository<HallRent>();

            var hallRents = await hallRentRepo
                                    .GetAllAsync(q =>
                                    q.Where(hr =>
                                    !hr.IsCanceled &&
                                    hr.StartDate > DateTime.Now.AddHours(3) &&
                                    allCopiesOfHallId.Contains(hr.HallId)));

            // cancel all hall rents 
            foreach (var hallRent in hallRents)
            {
                // send mail about canceled hall rents !!!
                hallRent.CancelDate = DateTime.Now;
                hallRent.IsCanceled = true;
                hallRentRepo.Update(hallRent);
            }
        }

        

        public async Task<Result<Hall>> MakeCopyOfHall(int hallId)
        {
            var hallEntity = await _repository.GetOneAsync(hallId);
            if (hallEntity == null || !hallEntity.IsVisible)
                return Result<Hall>.Failure(Error.NotFound);

            hallEntity.DefaultId = hallEntity.Id;

            // Copy Hall
            _repository.Detach(hallEntity);
            hallEntity.Id = 0;

            // Copy Hall Details
            _unitOfWork.GetRepository<HallDetails>().Detach(hallEntity.HallDetails!);
            hallEntity.HallDetails!.Id = 0;

            // Copy Seats
            foreach(var seat in hallEntity.Seats)
            {
                _unitOfWork.GetRepository<Seat>().Detach(seat);
                seat.Id = 0;
            }
            hallEntity.Events = [];
            hallEntity.Rents = [];
            hallEntity.IsCopy = true;
            hallEntity.IsVisible = false;

            await _repository.AddAsync(hallEntity);
            await _unitOfWork.SaveChangesAsync();

            return Result<Hall>.Success(hallEntity);
        }      


        protected sealed override IEnumerable<HallResponseDto> MapAsDto(IEnumerable<Hall> records)
        {
            return records.Select(entity =>
            {
                var responseDto = entity.AsDto<HallResponseDto>();
                responseDto.Type = entity.Type.AsDto<HallTypeResponseDto>();
                return responseDto;
            });
        }
        protected sealed override HallResponseDto MapAsDto(Hall entity)
        {
            var responseDto = entity.AsDto<HallResponseDto>();
            responseDto.HallDetails = entity.HallDetails?.AsDto<HallDetailsResponseDto>();
            responseDto.Type = entity.Type.AsDto<HallTypeResponseDto>();
            responseDto.Type.Equipments = entity.Type.Equipments.Select(eq =>
            {
                var equipmentDto = eq.AsDto<EquipmentResponseDto>();
                return equipmentDto;
            }).ToList();

            responseDto.Seats = entity.Seats.Select(seat =>
            {
                var seatDto = seat.AsDto<SeatResponseDto>();
                seatDto.SeatType = seat.SeatType.AsDto<SeatTypeResponseDto>();
                return seatDto;
            }).ToList();

            return responseDto;
        }

        protected sealed override Hall MapAsEntity(HallRequestDto requestDto)
        {
            var hallEntity = base.MapAsEntity(requestDto);
            hallEntity.HallDetails = requestDto.HallDetails.AsEntity<HallDetails>();

            hallEntity.HallDetails!.NumberOfSeats = hallEntity.Seats.Count;
            hallEntity.HallDetails!.MaxNumberOfSeats = hallEntity.HallDetails!.MaxNumberOfSeatsRows * hallEntity.HallDetails!.MaxNumberOfSeatsColumns;
            var area = requestDto!.HallDetails.TotalLength * requestDto!.HallDetails.TotalWidth;
            hallEntity.HallDetails!.TotalArea = Math.Round(area, 2);

            hallEntity.Seats = requestDto.Seats.Select(seat =>
            {
                var seatEntity = seat.AsEntity<Seat>();
                return seatEntity;
            }).ToList();
            return hallEntity;
        }

        private IEntity MapToEntity(HallRent_HallRequestDto requestDto, Hall oldEntity)
        {
            var hallEntity = (Hall)requestDto.MapTo(oldEntity);
            hallEntity.HallDetails = (HallDetails)requestDto.HallDetails.MapTo(oldEntity.HallDetails!);

            hallEntity.HallDetails!.NumberOfSeats = hallEntity.Seats.Count;
            hallEntity.HallDetails!.MaxNumberOfSeats = hallEntity.HallDetails!.MaxNumberOfSeatsRows * hallEntity.HallDetails!.MaxNumberOfSeatsColumns;

            hallEntity.Seats = MapSeats(oldEntity.Seats, requestDto.Seats);

            return hallEntity;
        }


        private IEntity MapToEntity(EventHallRequestDto requestDto, Hall oldEntity)
        {
            var hallEntity = (Hall)requestDto.MapTo(oldEntity);
            hallEntity.HallDetails = (HallDetails)requestDto.HallDetails.MapTo(oldEntity.HallDetails!);

            hallEntity.HallDetails!.NumberOfSeats = hallEntity.Seats.Count;
            hallEntity.HallDetails!.MaxNumberOfSeats = hallEntity.HallDetails!.MaxNumberOfSeatsRows * hallEntity.HallDetails!.MaxNumberOfSeatsColumns;

            hallEntity.Seats = MapSeats(oldEntity.Seats, requestDto.Seats);

            return hallEntity;
        }

       
        protected sealed override IEntity MapToEntity(HallRequestDto requestDto, Hall oldEntity)
        {
            var hallEntity = (Hall)requestDto.MapTo(oldEntity);
            hallEntity.HallDetails = (HallDetails)requestDto.HallDetails.MapTo(oldEntity.HallDetails!);

            hallEntity.HallDetails!.NumberOfSeats = hallEntity.Seats.Count;
            hallEntity.HallDetails!.MaxNumberOfSeats = hallEntity.HallDetails!.MaxNumberOfSeatsRows * hallEntity.HallDetails!.MaxNumberOfSeatsColumns;
            var area = requestDto!.HallDetails.TotalLength * requestDto!.HallDetails.TotalWidth;
            hallEntity.HallDetails!.TotalArea = Math.Round(area, 2);

            hallEntity.Seats = MapSeats(oldEntity.Seats, requestDto.Seats);

            return hallEntity;
        }


        private async Task<Error> ValidateBeforeUpdateHallForRent(HallRent_HallRequestDto? hallRequestDto, int rentId)
        {
            var userResult = await _userService.GetCurrentUser();
            if (!userResult.IsSuccessful)
                return userResult.Error;

            var user = userResult.Value;
            if (!user.UserRoles.Contains(Roles.Admin))
                return AuthError.UserDoesNotHaveSpecificRole;

            if (rentId < 0)
                return Error.QueryParamOutOfRange;

            if (hallRequestDto == null)
                return Error.NullParameter;

            var rentEntity = await _unitOfWork.GetRepository<HallRent>().GetOneAsync(rentId);
            if (rentEntity == null)
                return HallRentError.NotFound;

            if (rentEntity.IsCanceled)
                return HallRentError.HallRentIsCanceled;

            if (rentEntity.IsExpired)
                return HallRentError.HallRentIsExpired;

            if (!await IsEntityExistInDB<HallType>(hallRequestDto.HallTypeId))
                return HallError.HallTypeNotFound;

            if (!IsSeatNumbersInHallAreUnique(hallRequestDto.Seats))
                return HallError.SeatNumbersInHallAreNotUniqe;

            if (!await IsAllSeatTypesExistInDB(hallRequestDto.Seats))
                return SeatTypeError.SeatTypeNotFound;


            var isValidSeatsRowAndColumnError = IsValidSeatsRowAndColumn(hallRequestDto.Seats, hallRequestDto.HallDetails);
            if (isValidSeatsRowAndColumnError != Error.None)
                return isValidSeatsRowAndColumnError;

            return Error.None;
        }


        private async Task<Error> ValidateBeforeUpdateHallForEvent(EventHallRequestDto? hallRequestDto, int eventId)
        {
            var userResult = await _userService.GetCurrentUser();
            if (!userResult.IsSuccessful)
                return userResult.Error;

            var user = userResult.Value;
            if (!user.UserRoles.Contains(Roles.Admin))
                return AuthError.UserDoesNotHaveSpecificRole;

            if (eventId < 0)
                return Error.QueryParamOutOfRange;

            if (hallRequestDto == null)
                return Error.NullParameter;

            var eventEntity = await _unitOfWork.GetRepository<Event>().GetOneAsync(eventId);
            if (eventEntity == null)
                return EventError.NotFound;

            if (eventEntity.IsCanceled)
                return EventError.EventIsCanceled;

            if (eventEntity.IsExpired)
                return EventError.EventIsExpired;

            if (!await IsEntityExistInDB<HallType>(hallRequestDto.HallTypeId))
                return HallError.HallTypeNotFound;

            if (!IsSeatNumbersInHallAreUnique(hallRequestDto.Seats))
                return HallError.SeatNumbersInHallAreNotUniqe;

            if (!await IsAllSeatTypesExistInDB(hallRequestDto.Seats))
                return SeatTypeError.SeatTypeNotFound;


            var isValidSeatsRowAndColumnError = IsValidSeatsRowAndColumn(hallRequestDto.Seats, hallRequestDto.HallDetails);
            if (isValidSeatsRowAndColumnError != Error.None)
                return isValidSeatsRowAndColumnError;

            var changedSeats = GetListOfChangedSeats(eventEntity.Hall.Seats, hallRequestDto.Seats);

            var haveNotAvailableSeatChanged = changedSeats.Any(seat =>
                _seatService.IsSeatHaveActiveReservationForEvent(seat, eventEntity));

            if (haveNotAvailableSeatChanged)
                return SeatError.NotAvailableSeatChanged;

            return Error.None;
        }

        
        private static List<Seat> GetListOfChangedSeats(ICollection<Seat> seatsFromEntity, ICollection<SeatRequestDto> seatsFromRequest)
        {
            List<Seat> changedSeats = [];

            foreach (var seat in seatsFromEntity)
            {
                var isSeatNoChanged = seatsFromRequest.Any(s =>
                                        s.SeatNr == seat.SeatNr &&
                                        s.Row == seat.Row &&
                                        s.GridRow == seat.GridRow &&
                                        s.Column == seat.Column &&
                                        s.GridColumn == seat.GridColumn &&
                                        s.SeatTypeId == seat.SeatTypeId);
                if (!isSeatNoChanged)
                    changedSeats.Add(seat);
            }

            return changedSeats;
        }



        protected sealed override async Task<Error> ValidateEntity(HallRequestDto? requestDto, int? id = null)
        {
            var userResult = await _userService.GetCurrentUser();
            if (!userResult.IsSuccessful)
                return userResult.Error;

            var user = userResult.Value;
            if (!user.UserRoles.Contains(Roles.Admin))
                return AuthError.UserDoesNotHaveSpecificRole;

            var baseValidationError = await base.ValidateEntity(requestDto, id);
            if (baseValidationError != Error.None)
                return baseValidationError;

            if (!await IsEntityExistInDB<HallType>(requestDto!.HallTypeId))
                return HallError.HallTypeNotFound;

            if (!IsSeatNumbersInHallAreUnique(requestDto!.Seats))
                return HallError.SeatNumbersInHallAreNotUniqe;

            if (!await IsAllSeatTypesExistInDB(requestDto!.Seats))
                return SeatTypeError.SeatTypeNotFound;

            // Maybe hall details validation

            var isValidSeatsRowAndColumnError = IsValidSeatsRowAndColumn(requestDto.Seats, requestDto.HallDetails);
            if (isValidSeatsRowAndColumnError != Error.None)
                return isValidSeatsRowAndColumnError;

            return Error.None;
        }


        private bool IsSeatNumbersInHallAreUnique(ICollection<SeatRequestDto> seats) =>
            seats.Count == seats.Select(seat => seat.SeatNr).Distinct().Count();

        private async Task<bool> IsAllSeatTypesExistInDB(ICollection<SeatRequestDto> seats)
        {
            var seatTypeIds = seats.Select(seat => seat.SeatTypeId).Distinct().ToList();
            foreach (var seatTypeId in seatTypeIds)
            {
                if (!await IsEntityExistInDB<SeatType>(seatTypeId))
                    return false;
            }
            return true;
        }


        private static Error IsValidSeatsRowAndColumn(ICollection<SeatRequestDto> seatsFromRequest, IHallDetailsRequestDto hallDetails)
        {
            foreach (var seat in seatsFromRequest)
            {
                if (seat.Column > hallDetails.NumberOfSeatsColumns)
                    return SeatError.SeatColumnOutOfRange;

                if (seat.GridColumn > hallDetails.MaxNumberOfSeatsColumns)
                    return SeatError.SeatGridColumnOutOfRange;

                if (seat.Row > hallDetails.NumberOfSeatsRows)
                    return SeatError.SeatRowOutOfRange;

                if (seat.GridRow > hallDetails.MaxNumberOfSeatsRows)
                    return SeatError.SeatGridRowOutOfRange;

                var isSeatWithRowOrColumnExists = seatsFromRequest.Any(s =>
                                                    s != seat &&
                                                    s.Row == seat.Row &&
                                                    s.Column == seat.Column);
                if (isSeatWithRowOrColumnExists)
                    return SeatError.SeatWithSuchRowAndColumnAlreadyExist;

                var isSeatWithGridRowOrColumnExists = seatsFromRequest.Any(s =>
                                                        s != seat &&
                                                        s.GridRow == seat.GridRow &&
                                                        s.GridColumn == seat.GridColumn);

                if (isSeatWithGridRowOrColumnExists)
                    return SeatError.OtherSeatExistInSamePosition;
            }
            return Error.None;
        }



        private List<Seat> MapSeats(ICollection<Seat> seatsFromEntity, ICollection<SeatRequestDto> seatsFromRequest)
        {
            var seatDictionary = seatsFromEntity.ToDictionary(s => s.SeatNr);

            List<Seat> newSeats = [];
            Seat? seatEntity;

            foreach (var requestSeat in seatsFromRequest)
            {
                if (seatDictionary.TryGetValue(requestSeat.SeatNr, out var seat))
                {
                    seatEntity = (Seat)requestSeat.MapTo(seat);
                }
                else
                {
                    seatEntity = requestSeat.AsEntity<Seat>();
                }
                newSeats.Add(seatEntity);
            }
            return newSeats;
        }

        protected sealed override async Task<bool> IsSameEntityExistInDatabase(HallRequestDto entityDto, int? id = null)
        {
            return (await _repository.GetAllAsync(q =>
                                q.Where(entity =>
                                entity.Id != id &&
                                entity.IsVisible &&
                                entity.HallNr == entityDto.HallNr))).Any();
        }
    }
}
