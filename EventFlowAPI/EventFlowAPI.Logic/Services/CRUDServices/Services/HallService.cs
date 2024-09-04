using EventFlowAPI.DB.Entities;
using EventFlowAPI.DB.Entities.Abstract;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Mapper.Extensions;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces;
using EventFlowAPI.Logic.Services.CRUDServices.Services.BaseServices;
using EventFlowAPI.Logic.UnitOfWork;

namespace EventFlowAPI.Logic.Services.CRUDServices.Services
{
    public sealed class HallService(IUnitOfWork unitOfWork, ISeatService seatService) :
        GenericService<
            Hall,
            HallRequestDto,
            HallResponseDto
        >(unitOfWork),
    IHallService
    {
        private readonly ISeatService _seatService = seatService;
        // if it is update check before change numer of seats or delete seat that seat does not have reservation
        // before delete hall or update something check that there is no hall rent or event in the hall


        // If seat have reservation:
        // HallNr - If it change in case of update and seat have reservation user must be informed via email ticket will also be updated
        // If hall have 
        public async Task<Result<HallResponseDto>> UpdateHallForEvent(int hallId, int eventId, HallRequestDto? requestDto)
        {
            var _eventRepository = (IEventRepository)_unitOfWork.GetRepository<Event>();

            var result = await ValidateBeforeUpdateHallForEvent(_eventRepository, requestDto, eventId, hallId);
            if (!result.IsSuccessful)
            {
                return Result<HallResponseDto>.Failure(result.Error);
            }
            var hallEntity = result.Value.Hall;
            var eventEntity = result.Value.Event;
            var IsAnySeatChanged = result.Value.IsAnySeatChanged;

            if (IsAnySeatChanged || hallEntity.Seats.Count() != requestDto!.Seats.Count())
            {
                if (!hallEntity!.IsCopy)
                {
                    // Make copy of Hall
                    var copyResult = await MakeCopyOfHall(requestDto!, eventEntity, _eventRepository);
                    if (!copyResult.IsSuccessful)
                    {
                        return Result<HallResponseDto>.Failure(copyResult.Error);
                    }
                }
                else
                {
                    // Update existing copy
                    await UpdateAnExistingCopyOfHall(hallEntity, requestDto!);
                }
            }
            return Result<HallResponseDto>.Success();
        }

        private async Task UpdateAnExistingCopyOfHall(Hall hallEntity, HallRequestDto requestDto)
        {
            var newEntity = (Hall)MapToEntity(requestDto, hallEntity);
            newEntity.IsCopy = true;
            var preparedEntity = PrepareEntityForAddOrUpdate(newEntity, requestDto, hallEntity);

            _unitOfWork.GetRepository<Hall>().Update(preparedEntity);

            await _unitOfWork.SaveChangesAsync();
        }

        private async Task<Result<(Event Event, Hall Hall, bool IsAnySeatChanged)>> ValidateBeforeUpdateHallForEvent(
           IEventRepository _eventRepository,
           HallRequestDto? hallRequestDto,
           int eventId,
           int hallId)
        {
            if (eventId < 0)
            {
                return Result<(Event Event, Hall Hall, bool IsAnyChangedSeat)>
                    .Failure(Error.QueryParamOutOfRange);
            }

            var eventEntity = await _eventRepository.GetOneAsync(eventId);
            if (eventEntity == null)
            {
                return Result<(Event Event, Hall Hall, bool IsAnyChangedSeat)>
                    .Failure(EventError.NotFound);
            }

            if (eventEntity.HallId != hallId)
            {
                return Result<(Event Event, Hall Hall, bool IsAnyChangedSeat)>
                    .Failure(EventError.EventIsNotInSuchHall);
            }

            var error = await ValidateEntity(hallRequestDto, hallId);
            if (error != Error.None)
            {
                return Result<(Event Event, Hall Hall, bool IsAnyChangedSeat)>
                    .Failure(error);
            }

            var hallEntity = await _repository.GetOneAsync(hallId);
            if (hallEntity == null)
            {
                return Result<(Event Event, Hall Hall, bool IsAnyChangedSeat)>
                    .Failure(HallError.NotFound);
            }

            var changedSeats = GetListOfChangedSeats(hallEntity, hallRequestDto!);

            if (HaveNotAvailableSeatsChanged(changedSeats, eventEntity))
            {
                return Result<(Event Event, Hall Hall, bool IsAnyChangedSeat)>
                    .Failure(SeatError.NotAvailableSeatChanged);
            }

            return Result<(Event Event, Hall Hall, bool IsAnyChangedSeat)>
                    .Success((eventEntity, hallEntity, changedSeats.Count > 0));
        }

        public sealed override async Task<Result<HallResponseDto>> UpdateAsync(int id, HallRequestDto? requestDto)
        {
            var error = await ValidateEntity(requestDto, id);
            if (error != Error.None)
            {
                Result<HallResponseDto>.Failure(error);
            }

            var oldEntity = await _repository.GetOneAsync(id);
            if (oldEntity == null)
            {
                return Result<HallResponseDto>.Failure(Error.NotFound);
            }

            oldEntity.IsVisible = false;

            _repository.Update(oldEntity);
            await _unitOfWork.SaveChangesAsync();

            var newEntity = MapAsEntity(requestDto!);

            var preparedEntity = PrepareEntityForAddOrUpdate(newEntity, requestDto!);

            await _repository.AddAsync(preparedEntity);
            await _unitOfWork.SaveChangesAsync();

            return Result<HallResponseDto>.Success();
        }

        public sealed override async Task<Result<IEnumerable<HallResponseDto>>> GetAllAsync()
        {
            var records = await _repository.GetAllAsync(q => q.Where(entity => entity.IsVisible));
            var response = MapAsDto(records);

            return Result<IEnumerable<HallResponseDto>>.Success(response);
        }

        protected sealed override async Task<Error> ValidateEntity(HallRequestDto? requestDto, int? id = null)
        {
            var baseValidationError = await base.ValidateEntity(requestDto, id);

            if (baseValidationError != Error.None)
            {
                return baseValidationError;
            }

            if (!await IsEntityExistInDB<HallType>(requestDto!.HallTypeId))
            {
                return HallError.HallTypeNotFound;
            }

            if (!IsSeatNumbersInHallAreUnique(requestDto!.Seats))
            {
                return HallError.SeatNumbersInHallAreNotUniqe;
            }

            if (!await IsAllSeatTypesExistInDB(requestDto!.Seats))
            {
                return SeatError.SeatTypeNotFound;
            }

            var isValidSeatsRowAndColumnError = IsValidSeatsRowAndColumn(requestDto);
            if (isValidSeatsRowAndColumnError != Error.None)
            {
                return isValidSeatsRowAndColumnError;
            }

            return Error.None;
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
            hallEntity.Seats = requestDto.Seats.Select(seat =>
            {
                var seatEntity = seat.AsEntity<Seat>();
                return seatEntity;
            }).ToList();
            return hallEntity;
        }

        protected sealed override IEntity MapToEntity(HallRequestDto requestDto, Hall oldEntity)
        {
            var seats = oldEntity.Seats.ToList();
            var hallEntity = (Hall)base.MapToEntity(requestDto, oldEntity);

            var seatDictionary = seats.ToDictionary(s => s.SeatNr);

            List<Seat> newSeats = [];
            Seat? seatEntity;

            foreach (var requestSeat in requestDto.Seats)
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

            hallEntity.Seats = newSeats.ToList();

            return hallEntity;
        }

        protected sealed override async Task<bool> IsSameEntityExistInDatabase(HallRequestDto entityDto, int? id = null)
        {
            var entities = await _repository.GetAllAsync(q =>
                       q.Where(entity => entity.HallNr == entityDto.HallNr && entity.IsVisible && entityDto.IsVisible)
                   );

            return IsEntityWithOtherIdExistInList(entities, id);
        }

        protected sealed override Hall PrepareEntityForAddOrUpdate(Hall newEntity, HallRequestDto requestDto, Hall? oldEntity = null)
        {
            newEntity.NumberOfSeats = newEntity.Seats.Count;
            newEntity.MaxNumberOfSeats = newEntity.MaxNumberOfSeatsRows * newEntity.MaxNumberOfSeatsColumns;
            return newEntity;
        }
        private async Task<Result<HallResponseDto>> MakeCopyOfHall(HallRequestDto requestDto, Event eventEntity, IEventRepository _eventRepository)
        {
            requestDto!.IsCopy = true;
            requestDto!.IsVisible = false;

            var hallAddResult = await AddAsync(requestDto);
            if (!hallAddResult.IsSuccessful)
            {
                return Result<HallResponseDto>.Failure(hallAddResult.Error);
            }

            var eventTicketsIds = eventEntity.Tickets.Select(t => t.Id);

            var reservationsForEvent = (await _unitOfWork.GetRepository<Reservation>().GetAllAsync(q =>
                                        q.Where(r => eventTicketsIds.Contains(r.EventTicketId)))).ToList();

            var seatsInHall = await _unitOfWork.GetRepository<Seat>().GetAllAsync(q =>
                                q.Where(s => s.HallId == hallAddResult.Value.Id));

            foreach (var reservation in reservationsForEvent)
            {
                reservation.Seats = reservation.Seats.Select(seat =>
                {
                    var newSeat = seatsInHall.FirstOrDefault(s => s.SeatNr == seat.SeatNr);
                    if (newSeat != null)
                    {
                        return newSeat;
                    }
                    return seat;

                }).ToList();
                _unitOfWork.GetRepository<Reservation>().Update(reservation);
            }

            eventEntity.HallId = hallAddResult.Value.Id;
            _eventRepository.Update(eventEntity);
            await _unitOfWork.SaveChangesAsync();

            return Result<HallResponseDto>.Success();
        }




        private static List<Seat> GetListOfChangedSeats(Hall entity, HallRequestDto requestDto)
        {
            List<Seat> seats = [];

            foreach (var seat in entity.Seats)
            {
                var isSameSeatExistInSamePlace = requestDto.Seats.Any(s => s.SeatNr == seat.SeatNr &&
                                                                             s.Row == seat.Row &&
                                                                             s.GridRow == seat.GridRow &&
                                                                             s.Column == seat.Column &&
                                                                             s.GridColumn == seat.GridColumn &&
                                                                             s.SeatTypeId == seat.SeatTypeId);
                if (!isSameSeatExistInSamePlace)
                {
                    seats.Add(seat);
                }
            }

            return seats;
        }

        private static Error IsValidSeatsRowAndColumn(HallRequestDto requestDto)
        {
            foreach (var seat in requestDto.Seats)
            {
                if (seat.Column > requestDto.NumberOfSeatsColumns)
                {
                    return SeatError.SeatColumnOutOfRange;
                }
                if (seat.GridColumn > requestDto.MaxNumberOfSeatsColumns)
                {
                    return SeatError.SeatGridColumnOutOfRange;
                }
                if (seat.Row > requestDto.NumberOfSeatsRows)
                {
                    return SeatError.SeatRowOutOfRange;
                }
                if (seat.GridRow > requestDto.MaxNumberOfSeatsRows)
                {
                    return SeatError.SeatGridRowOutOfRange;
                }
                if (requestDto.Seats.Any(s => s != seat && s.Row == seat.Row && s.Column == seat.Column))
                {
                    return SeatError.SeatWithSuchRowAndColumnAlreadyExist;
                }
                if (requestDto.Seats.Any(s => s != seat && s.GridRow == seat.GridRow && s.GridColumn == seat.GridColumn))
                {
                    return SeatError.OtherSeatExistInSamePosition;
                }
            }
            return Error.None;
        }
        private async Task<bool> IsAllSeatTypesExistInDB(ICollection<SeatRequestDto> seats)
        {
            var seatTypeIds = GetDistinctSeatTypeIds(seats);
            foreach (var seatTypeId in seatTypeIds)
            {
                if (!await IsEntityExistInDB<SeatType>(seatTypeId))
                {
                    return false;
                }
            }
            return true;
        }

        private static List<int> GetDistinctSeatTypeIds(IEnumerable<SeatRequestDto> seats) =>
            seats.Select(seat => seat.SeatTypeId).Distinct().ToList();

        private static bool IsSeatNumbersInHallAreUnique(ICollection<SeatRequestDto> seats) =>
            seats.Count == seats.Select(seat => seat.SeatNr).Distinct().Count();

        private bool HaveNotAvailableSeatsChanged(List<Seat> seats, Event eventEntity) =>
            seats.Any(seat => _seatService.IsSeatHaveActiveReservationForEvent(seat, eventEntity));
    }
}
