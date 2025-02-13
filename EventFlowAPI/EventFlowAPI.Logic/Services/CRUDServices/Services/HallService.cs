﻿using EventFlowAPI.DB.Entities;
using EventFlowAPI.DB.Entities.Abstract;
using EventFlowAPI.Logic.DTO.Abstract;
using EventFlowAPI.Logic.DTO.Interfaces;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.DTO.UpdateRequestDto;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Extensions;
using EventFlowAPI.Logic.Identity.Helpers;
using EventFlowAPI.Logic.Identity.Services.Interfaces;
using EventFlowAPI.Logic.Mapper.Extensions;
using EventFlowAPI.Logic.Query;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces;
using EventFlowAPI.Logic.Services.CRUDServices.Services.BaseServices;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces;
using EventFlowAPI.Logic.UnitOfWork;
using Serilog;

namespace EventFlowAPI.Logic.Services.CRUDServices.Services
{
    public sealed class HallService(
        IUnitOfWork unitOfWork,
        IAuthService authService,
        ISeatService seatService,
        IReservationService reservationService,
        IEmailSenderService emailSender, 
        IFestivalService festivalService,
        ITicketService ticketService,
        IHallRentService hallRentService,
        IHallTypeService hallTypeService,
        IFileService fileService) :
        GenericService<
            Hall,
            HallRequestDto,
            UpdateHallRequestDto,
            HallResponseDto,
            HallQuery
        >(unitOfWork, authService),
    IHallService
    {
        private readonly ISeatService _seatService = seatService;
        private readonly IReservationService _reservationService = reservationService;
        private readonly IEmailSenderService _emailSender = emailSender;
        private readonly IFestivalService _festivalService = festivalService;
        private readonly ITicketService _ticketService = ticketService;
        private readonly IHallRentService _hallRentService = hallRentService;
        private readonly IFileService _fileService = fileService;
        private readonly IHallTypeService _hallTypeService = hallTypeService;

        public sealed override async Task<Result<IEnumerable<HallResponseDto>>> GetAllAsync(HallQuery query)
        {
            var records = await _repository.GetAllAsync(q => q.Where(entity => entity.IsVisible)
                                                              .ByQuery(query)
                                                              .GetPage(query.PageNumber, query.PageSize));
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

            // HallView PDF
            var hallViewFileNameResult = await _fileService.CreateHallViewPDF(hallEntity, null, null, isUpdate: false);
            if (!hallViewFileNameResult.IsSuccessful)
                return Result<HallResponseDto>.Failure(hallViewFileNameResult.Error);
            var hallViewPDFFileName = hallViewFileNameResult.Value;
            hallEntity.HallViewFileName = hallViewPDFFileName;

            _repository.Update(hallEntity);
            await _unitOfWork.SaveChangesAsync();

            var hallEntityDto = MapAsDto(hallEntity);

            return Result<HallResponseDto>.Success(hallEntityDto);
        }

        public sealed override async Task<Result<HallResponseDto>> UpdateAsync(int id, UpdateHallRequestDto? requestDto)
        {
            var error = await ValidateEntity(requestDto, id);
            if (error != Error.None)
                return Result<HallResponseDto>.Failure(error);

            var hallEntity = await _repository.GetOneAsync(id);
            if (hallEntity == null)
                return Result<HallResponseDto>.Failure(Error.NotFound);

            if (!hallEntity.IsVisible)
                return Result<HallResponseDto>.Failure(Error.NotFound);

            if (hallEntity.HallNr != requestDto!.HallNr /*||
                hallEntity.Floor != requestDto!.Floor ||
                hallEntity.HallDetails!.TotalLength != requestDto!.HallDetails!.TotalLength ||
                hallEntity.HallDetails!.TotalWidth != requestDto!.HallDetails!.TotalWidth*/)
            {
                await UpdateHallCopies(hallEntity, requestDto);
            }
            await _unitOfWork.SaveChangesAsync();

            var oldHallEntity = new Hall
            {
                HallNr = hallEntity.HallNr,
                HallDetails = new HallDetails
                {
                    TotalLength = hallEntity.HallDetails!.TotalLength,
                    TotalWidth = hallEntity.HallDetails!.TotalWidth,
                }
            };

            var newHallEntity = (Hall)MapToEntity(requestDto, hallEntity);

            Log.Information("Creating new Hall entity with the following properties:");
            Log.Information("DefaultId: {DefaultId}, HallNr: {HallNr}, RentalPricePerHour: {RentalPricePerHour}, IsCopy: {IsCopy}, IsVisible: {IsVisible}, IsUpdated: {IsUpdated}, UpdateDate: {UpdateDate}, Floor: {Floor}, HallViewFileName: {HallViewFileName}, HallTypeId: {HallTypeId}",
                newHallEntity.DefaultId,
                newHallEntity.HallNr,
                newHallEntity.RentalPricePerHour,
                newHallEntity.IsCopy,
                newHallEntity.IsVisible,
                newHallEntity.IsUpdated,
                newHallEntity.UpdateDate,
                newHallEntity.Floor,
                newHallEntity.HallViewFileName,
                newHallEntity.HallTypeId);

            if (newHallEntity.HallDetails != null)
            {
                Log.Information("HallDetails: TotalLength: {TotalLength}, TotalWidth: {TotalWidth}, TotalArea: {TotalArea}, StageLength: {StageLength}, StageWidth: {StageWidth}, MaxNumberOfSeatsRows: {MaxNumberOfSeatsRows}, MaxNumberOfSeatsColumns: {MaxNumberOfSeatsColumns}, NumberOfSeats: {NumberOfSeats}, MaxNumberOfSeats: {MaxNumberOfSeats}",
                    newHallEntity.HallDetails.TotalLength,
                    newHallEntity.HallDetails.TotalWidth,
                    newHallEntity.HallDetails.TotalArea,
                    newHallEntity.HallDetails.StageLength,
                    newHallEntity.HallDetails.StageWidth,
                    newHallEntity.HallDetails.MaxNumberOfSeatsRows,
                    newHallEntity.HallDetails.MaxNumberOfSeatsColumns,
                    newHallEntity.HallDetails.NumberOfSeats,
                    newHallEntity.HallDetails.MaxNumberOfSeats);
            }
            else
            {
                Log.Warning("HallDetails is null.");
            }

            if (newHallEntity.Type != null)
            {
                Log.Information("HallType: Name: {Name}, Description: {Description}, IsUpdated: {IsUpdated}, UpdateDate: {UpdateDate}, IsDeleted: {IsDeleted}, HallTypeGuid: {HallTypeGuid}, DeleteDate: {DeleteDate}, IsSoftUpdated: {IsSoftUpdated}, PhotoName: {PhotoName}",
                    newHallEntity.Type.Name,
                    newHallEntity.Type.Description,
                    newHallEntity.Type.IsUpdated,
                    newHallEntity.Type.UpdateDate,
                    newHallEntity.Type.IsDeleted,
                    newHallEntity.Type.HallTypeGuid,
                    newHallEntity.Type.DeleteDate,
                    newHallEntity.Type.IsSoftUpdated,
                    newHallEntity.Type.PhotoName);
            }
            else
            {
                Log.Warning("HallType is null.");
            }
            foreach(var seat in newHallEntity.Seats)
            {
                if (seat == null)
                {
                    Log.Warning("Seat object is null.");
                }
                else
                {
                    // Logowanie właściwości głównego obiektu Seat
                    Log.Information("Logging Seat details:");
                    Log.Information("SeatNr: {SeatNr}, Row: {Row}, GridRow: {GridRow}, Column: {Column}, GridColumn: {GridColumn}, SeatTypeId: {SeatTypeId}, HallId: {HallId}, IsUpdated: {IsUpdated}, UpdateDate: {UpdateDate}",
                        seat.SeatNr,
                        seat.Row,
                        seat.GridRow,
                        seat.Column,
                        seat.GridColumn,
                        seat.SeatTypeId,
                        seat.HallId,
                        seat.IsUpdated,
                        seat.UpdateDate);

                    // Logowanie właściwości powiązanego obiektu SeatType
                    if (seat.SeatType != null)
                    {
                        Log.Information("SeatType details:");
                        Log.Information("Name: {Name}, Description: {Description}, IsDeleted: {IsDeleted}, DeleteDate: {DeleteDate}, IsUpdated: {IsUpdated}, UpdateDate: {UpdateDate}, IsSoftUpdated: {IsSoftUpdated}, AddtionalPaymentPercentage: {AddtionalPaymentPercentage}, SeatColor: {SeatColor}",
                            seat.SeatType.Name,
                            seat.SeatType.Description,
                            seat.SeatType.IsDeleted,
                            seat.SeatType.DeleteDate,
                            seat.SeatType.IsUpdated,
                            seat.SeatType.UpdateDate,
                            seat.SeatType.IsSoftUpdated,
                            seat.SeatType.AddtionalPaymentPercentage,
                            seat.SeatType.SeatColor);
                    }
                    else
                    {
                        Log.Warning("SeatType is null for SeatNr: {SeatNr}.", seat.SeatNr);
                    }

                    Log.Information("Finished logging Seat details.");

                }
            }

            //decimal area = requestDto!.HallDetails.TotalLength * requestDto!.HallDetails.TotalWidth;
            //newHallEntity.HallDetails!.TotalArea = Math.Round(area, 2);

            // HallView Update
            var hallViewFileNameResult = await _fileService.CreateHallViewPDF(newHallEntity, null, null, isUpdate: true);
            if (!hallViewFileNameResult.IsSuccessful)
                return Result<HallResponseDto>.Failure(hallViewFileNameResult.Error);

            _repository.Update(newHallEntity);
            await _unitOfWork.SaveChangesAsync();

            // Update Reservations for events in hall
            if (hallEntity.HallNr != requestDto!.HallNr)
            {
                var updateReservationError = await UpdateReservationsInCauseOfHallUpdate(hallEntity, oldHallEntity);
                if (updateReservationError != Error.None)
                    return Result<HallResponseDto>.Failure(updateReservationError);
            }

            // Update Hall Rents in hall
            if (hallEntity.HallNr != requestDto!.HallNr /*||
                hallEntity.HallDetails!.TotalLength != requestDto!.HallDetails!.TotalLength ||
                hallEntity.HallDetails!.TotalWidth != requestDto!.HallDetails!.TotalWidth*/)
            {
                var updateHallRentError = await UpdateHallRentsInCauseOfHallUpdate(hallEntity, oldHallEntity);
                if (updateHallRentError != Error.None)
                    return Result<HallResponseDto>.Failure(updateHallRentError);
            }

            var newHallEntityDto = MapAsDto(newHallEntity);
            return Result<HallResponseDto>.Success(newHallEntityDto);
        }

        public async Task<Result<HallResponseDto>> UpdateHallForEvent(int eventId, EventHallRequestDto? requestDto)
        {
            var validationError = await ValidateBeforeUpdateHallForEvent(requestDto, eventId);
            if (validationError != Error.None)
                return Result<HallResponseDto>.Failure(validationError);

            var eventEntity = await _unitOfWork.GetRepository<Event>().GetOneAsync(eventId);
            var hallEntity = await _unitOfWork.GetRepository<Hall>().GetOneAsync(eventEntity!.HallId);

            hallEntity = (Hall)MapToEntity(requestDto!, hallEntity!);

            // HallView Update
            var eventHallViewFileNameResult = await _fileService.CreateHallViewPDF(hallEntity, null, eventEntity, isUpdate: true);
            if (!eventHallViewFileNameResult.IsSuccessful)
                return Result<HallResponseDto>.Failure(eventHallViewFileNameResult.Error);

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
            var hallEntity = await _unitOfWork.GetRepository<Hall>().GetOneAsync(rentEntity!.HallId);

            hallEntity = (Hall)MapToEntity(requestDto!, hallEntity!);

            foreach(var seat in hallEntity.Seats)
            {
                var seatType = await _unitOfWork.GetRepository<SeatType>().GetOneAsync(seat.SeatTypeId);
                if (seatType != null)
                    seat.SeatType = seatType;
            }
            // HallView Update
            var eventHallViewFileNameResult = await _fileService.CreateHallViewPDF(hallEntity, rentEntity, null, isUpdate: true);
            if (!eventHallViewFileNameResult.IsSuccessful)
                return Result<HallResponseDto>.Failure(eventHallViewFileNameResult.Error);

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

            var hall = validationResult.Value.Entity;
            var user = validationResult.Value.User;

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
                                    !r.IsDeleted &&
                                    r.EndDate > DateTime.Now &&
                                    allCopiesOfHallId.Contains(r.Ticket.Event.HallId)));

                var eventsToDelete = await CancelEventsInHall(allCopiesOfHallId);
                var festivalsToDelete = await _festivalService.CancelFestivalIfEssential(eventsToDelete);
                await _ticketService.DeleteTickets(eventsToDelete, festivalsToDelete);
                await _unitOfWork.SaveChangesAsync();

                var cancelReservationError = await _reservationService.CancelReservationsInCauseOfDeleteEventOrHallOrFestival(reservations);
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


        private async Task UpdateHallCopies(Hall hallEntity, UpdateHallRequestDto requestDto)
        {
            var hallCopies = await _repository.GetAllAsync(q =>
                        q.Where(h =>
                        h.Id != hallEntity.Id &&
                        h.DefaultId == hallEntity.DefaultId));

            foreach (var hall in hallCopies)
            {
                hall.HallNr = requestDto!.HallNr;
                //hall.Floor = requestDto!.Floor;
                //hall.HallDetails!.TotalLength = requestDto!.HallDetails.TotalLength;
                //hall.HallDetails!.TotalWidth = requestDto!.HallDetails.TotalWidth;
               // decimal copyHallArea = requestDto!.HallDetails.TotalLength * requestDto!.HallDetails.TotalWidth;
                //hall.HallDetails!.TotalArea = Math.Round(copyHallArea, 2);

                _repository.Update(hall);
            }
        }

        private async Task<Error> UpdateHallRentsInCauseOfHallUpdate(Hall hallEntity, Hall oldHallEntity)
        {
            var allActiveHallRents = await _unitOfWork.GetRepository<HallRent>().GetAllAsync(q =>
                                q.Where(hr =>
                                hr.Hall.DefaultId == hallEntity.DefaultId &&
                                !hr.IsDeleted &&
                                hr.EndDate > DateTime.Now));
            if (allActiveHallRents.Any())
            {
                var sendMailError = await _hallRentService.SendMailsAboutUpdatedHallRents(allActiveHallRents, oldHallEntity);
                if (sendMailError != Error.None)
                    return sendMailError;
            }
            return Error.None;
        }
        private async Task<Error> UpdateReservationsInCauseOfHallUpdate(Hall hallEntity, Hall oldHallEntity)
        {
            var allActiveReservations = await _unitOfWork.GetRepository<Reservation>().GetAllAsync(q =>
                                            q.Where(r =>
                                            !r.IsDeleted &&
                                            r.EndDate > DateTime.Now &&
                                            r.Ticket.Event.Hall.DefaultId == hallEntity.DefaultId));
            if (allActiveReservations.Any())
            {
                var eventsIds = allActiveReservations.Select(r => r.Ticket.EventId).Distinct();
                var eventList = await _unitOfWork.GetRepository<Event>().GetAllAsync(q => q.Where(e => eventsIds.Contains(e.Id)));
                foreach (var eventEntity in eventList)
                {
                    var hall = eventEntity.Hall;

                    // HallView Update
                    var eventHallViewFileNameResult = await _fileService.CreateHallViewPDF(hall, null, eventEntity, isUpdate: true);
                    if (!eventHallViewFileNameResult.IsSuccessful)
                        return eventHallViewFileNameResult.Error;
                }

                var sendMailError = await _reservationService.SendMailsAboutUpdatedReservations(allActiveReservations, oldHallEntity, hallEntity);
                if (sendMailError != Error.None)
                    return sendMailError;
            }
            return Error.None;
        }

        private async Task<ICollection<Event>> CancelEventsInHall(IEnumerable<int> allCopiesOfHallId)
        {
            var eventsToDelete = await _unitOfWork.GetRepository<Event>()
                                .GetAllAsync(q =>
                                q.Where(e =>
                                !e.IsDeleted &&
                                e.EndDate > DateTime.Now &&
                                allCopiesOfHallId.Contains(e.HallId)));

            foreach (var eventEntity in eventsToDelete)
            {
                eventEntity.DeleteDate = DateTime.Now;
                eventEntity.IsDeleted = true;
                _unitOfWork.GetRepository<Event>().Update(eventEntity);
            }

            return eventsToDelete.ToList();
        }



        private async Task<Error> CancelHallRentsOnHall(List<int> allCopiesOfHallId)
        {
            var hallRentRepo = _unitOfWork.GetRepository<HallRent>();

            var hallRentsToDelete = await hallRentRepo
                                    .GetAllAsync(q =>
                                    q.Where(hr =>
                                    !hr.IsDeleted &&
                                    hr.StartDate > DateTime.Now.AddHours(3) &&
                                    allCopiesOfHallId.Contains(hr.HallId)));

            // Cancel all Hall Rents 
            foreach (var hallRent in hallRentsToDelete)
            {
                var deleteError = await _hallRentService.SoftDeleteHallRent(hallRent);
                if (deleteError != Error.None)
                    return deleteError;
            }
            await _unitOfWork.SaveChangesAsync();
            
            // Send info about canceled Hall Rents
            var hallRentsGroupByUser = hallRentsToDelete.GroupBy(r => r.UserId)
                                            .Select(g => new
                                            {
                                                UserId = g.Key,
                                                HallRents = g.ToList(),
                                            });

            foreach(var group in hallRentsGroupByUser)
            {
                var userHallRents = group.HallRents;
                await _emailSender.SendInfoAboutCanceledHallRents(userHallRents);
            }

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


        private async Task<Error> ValidateBeforeUpdateHallForRent(HallRent_HallRequestDto? hallRequestDto, int rentId)
        {
            if (rentId < 0)
                return Error.QueryParamOutOfRange;

            if (hallRequestDto == null)
                return Error.NullParameter;

            var rentEntity = await _unitOfWork.GetRepository<HallRent>().GetOneAsync(rentId);
            if (rentEntity == null)
                return HallRentError.NotFound;

            var hall = await _unitOfWork.GetRepository<Hall>().GetOneAsync(rentEntity.HallId);
            if (hall == null)
                return HallError.NotFound;

            var userResult = await _authService.GetCurrentUser();
            if (!userResult.IsSuccessful)
                return userResult.Error;

            var user = userResult.Value;
            if (!user.IsInRole(Roles.Admin))
            {
                if(rentEntity.UserId != user.Id)
                    return AuthError.UserDoesNotHaveSpecificRole;
            }
                

            if (rentEntity.IsDeleted)
                return HallRentError.HallRentIsDeleted;

            if (rentEntity.IsExpired)
                return HallRentError.HallRentIsExpired;

           /* if (!await IsEntityExistInDB<HallType>(hallRequestDto.HallTypeId))
                return HallError.HallTypeNotFound;*/

            if (!IsSeatNumbersInHallAreUnique(hallRequestDto.Seats))
                return HallError.SeatNumbersInHallAreNotUniqe;

            if (!await IsAllSeatTypesExistInDB(hallRequestDto.Seats))
                return SeatTypeError.SeatTypeNotFound;

            var hallDetailsRequestDto = new HallDetailsRequestDto
            {
                StageLength = hall.HallDetails.StageLength,
                StageWidth = hall.HallDetails.StageWidth,
                MaxNumberOfSeatsRows = hall.HallDetails.MaxNumberOfSeatsRows,
                MaxNumberOfSeatsColumns = hall.HallDetails.MaxNumberOfSeatsColumns
            };

            var isValidSeatsRowAndColumnError = IsValidSeatsRowAndColumn(hallRequestDto.Seats, hallDetailsRequestDto);
            if (isValidSeatsRowAndColumnError != Error.None)
                return isValidSeatsRowAndColumnError;

            return Error.None;
        }

        private async Task<Error> ValidateBeforeUpdateHallForEvent(EventHallRequestDto? hallRequestDto, int eventId)
        {
            var userResult = await _authService.GetCurrentUser();
            if (!userResult.IsSuccessful)
                return userResult.Error;

            var user = userResult.Value;
            if (!user.IsInRole(Roles.Admin))
                return AuthError.UserDoesNotHaveSpecificRole;

            if (eventId < 0)
                return Error.QueryParamOutOfRange;

            if (hallRequestDto == null)
                return Error.NullParameter;

            var eventEntity = await _unitOfWork.GetRepository<Event>().GetOneAsync(eventId);
            if (eventEntity == null)
                return EventError.NotFound;

            if (eventEntity.IsDeleted)
                return EventError.EventIsDeleted;

            if (eventEntity.IsExpired)
                return EventError.EventIsExpired;

            /*if (!await IsEntityExistInDB<HallType>(hallRequestDto.HallTypeId))
                return HallError.HallTypeNotFound;*/

            if (!IsSeatNumbersInHallAreUnique(hallRequestDto.Seats))
                return HallError.SeatNumbersInHallAreNotUniqe;

            if (!await IsAllSeatTypesExistInDB(hallRequestDto.Seats))
                return SeatTypeError.SeatTypeNotFound;

            var hall = await _unitOfWork.GetRepository<Hall>().GetOneAsync(eventEntity.HallId);
            if (hall == null)
                return HallError.NotFound;

            var hallDetailsRequestDto = new HallDetailsRequestDto
            {
                StageLength = hall.HallDetails.StageLength,
                StageWidth = hall.HallDetails.StageWidth,
                MaxNumberOfSeatsRows = hall.HallDetails.MaxNumberOfSeatsRows,
                MaxNumberOfSeatsColumns = hall.HallDetails.MaxNumberOfSeatsColumns
            };


            var isValidSeatsRowAndColumnError = IsValidSeatsRowAndColumn(hallRequestDto.Seats, hallDetailsRequestDto);
            if (isValidSeatsRowAndColumnError != Error.None)
                return isValidSeatsRowAndColumnError;

            var changedSeats = GetListOfChangedSeats(hall!.Seats, hallRequestDto.Seats);

            var haveNotAvailableSeatChanged = changedSeats.Any(seat =>
                _seatService.IsSeatHaveActiveReservationForEvent(seat, eventEntity));

            if (haveNotAvailableSeatChanged)
                return SeatError.NotAvailableSeatChanged;

            return Error.None;
        }

        private static Error IsValidSeatsRowAndColumn(ICollection<SeatRequestDto> seatsFromRequest, IHallDetailsRequestDto? hallDetails)
        {
            foreach (var seat in seatsFromRequest)
            {
                /*if (seat.Column > hallDetails.NumberOfSeatsColumns)
                    return SeatError.SeatColumnOutOfRange;*/

                if (hallDetails != null && seat.GridColumn > hallDetails.MaxNumberOfSeatsColumns)
                    return SeatError.SeatGridColumnOutOfRange;

                /*if (seat.Row > hallDetails.NumberOfSeatsRows)
                    return SeatError.SeatRowOutOfRange;*/

                if (hallDetails != null && seat.GridRow > hallDetails.MaxNumberOfSeatsRows)
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

        protected sealed override async Task<Error> ValidateEntity(IRequestDto? requestDto, int? id = null)
        {
            var userResult = await _authService.GetCurrentUser();
            if (!userResult.IsSuccessful)
                return userResult.Error;

            var user = userResult.Value;
            if (!user.IsInRole(Roles.Admin))
                return AuthError.UserDoesNotHaveSpecificRole;

            if (id != null && id < 0)
                return Error.RouteParamOutOfRange;

            if (requestDto == null)
                return Error.NullParameter;

            var isSameEntityExistsResult = await IsSameEntityExistInDatabase(requestDto, id);
            if (!isSameEntityExistsResult.IsSuccessful) return isSameEntityExistsResult.Error;

            var isSameEntityExistInDb = isSameEntityExistsResult.Value;
            if (isSameEntityExistInDb)
                return Error.SuchEntityExistInDb;

            int hallTypeId;
            ICollection<SeatRequestDto> seats = [];
            HallDetailsRequestDto? details;
            switch (requestDto)
            {
                case HallRequestDto hallRequestDto:
                    hallTypeId = hallRequestDto.HallTypeId;
                    seats = hallRequestDto.Seats;
                    details = hallRequestDto.HallDetails;
                    break;
                case UpdateHallRequestDto updateHallRequestDto:
                    hallTypeId = updateHallRequestDto.HallTypeId;
                    seats = updateHallRequestDto.Seats;
                    details = null;
                    //details = updateHallRequestDto.HallDetails;
                    break;
                default:
                    return Error.BadRequestType;
            }

            if (!await IsEntityExistInDB<HallType>(hallTypeId))
                return HallError.HallTypeNotFound;

            if (!IsSeatNumbersInHallAreUnique(seats))
                return HallError.SeatNumbersInHallAreNotUniqe;

            if (!await IsAllSeatTypesExistInDB(seats))
                return SeatTypeError.SeatTypeNotFound;

            // Maybe hall details validation

            var isValidSeatsRowAndColumnError = IsValidSeatsRowAndColumn(seats, details);
            if (isValidSeatsRowAndColumnError != Error.None)
                return isValidSeatsRowAndColumnError;

            return Error.None;
        }

        protected sealed override async Task<Result<bool>> IsSameEntityExistInDatabase(IRequestDto requestDto, int? id = null)
        {
            int hallNr;
            switch (requestDto)
            {
                case HallRequestDto hallRequestDto:
                    hallNr = hallRequestDto.HallNr;
                    break;
                case UpdateHallRequestDto updateHallRequestDto:
                    hallNr = updateHallRequestDto.HallNr;
                    break;
                default:
                    return Result<bool>.Failure(Error.BadRequestType);
            }

            var result = (await _repository.GetAllAsync(q =>
                                q.Where(entity =>
                                entity.Id != id &&
                                entity.IsVisible &&
                                entity.HallNr == hallNr))).Any();

            return Result<bool>.Success(result);
        }


        protected sealed override IEnumerable<HallResponseDto> MapAsDto(IEnumerable<Hall> records)
        {
            return records.Select(entity =>
            {
                var responseDto = entity.AsDto<HallResponseDto>();
                responseDto.HallDetails = null;
                responseDto.Type = _hallTypeService.MapAsDto(entity.Type);
                responseDto.Type.Equipments = [];
                responseDto.SeatsCount = entity.Seats.Count;
                responseDto.Seats = [];
                return responseDto;
            });
        }
        protected sealed override HallResponseDto MapAsDto(Hall entity)
        {
            var responseDto = entity.AsDto<HallResponseDto>();
            responseDto.HallDetails = entity.HallDetails?.AsDto<HallDetailsResponseDto>();
            if (responseDto.HallDetails?.StageLength != null && responseDto.HallDetails.StageWidth != null)
                responseDto.HallDetails.StageArea = (decimal)responseDto.HallDetails.StageLength * (decimal)responseDto.HallDetails.StageWidth;

            responseDto.Type = _hallTypeService.MapAsDto(entity.Type);
            responseDto.Type.Equipments = entity.Type.Equipments.Select(eq =>
            {
                var equipmentDto = eq.AsDto<EquipmentResponseDto>();
                return equipmentDto;
            }).ToList();
            responseDto.SeatsCount = entity.Seats.Count;
            responseDto.Seats = entity.Seats.Select(seat =>
            {
                var seatDto = seat.AsDto<SeatResponseDto>();
                seatDto.SeatType = seat.SeatType.AsDto<SeatTypeResponseDto>();
                return seatDto;
            }).ToList();

            return responseDto;
        }

        protected sealed override Hall MapAsEntity(IRequestDto requestDto)
        {
            HallDetailsRequestDto? hallDetails = default!;
            List<SeatRequestDto> seats = [];
            switch (requestDto)
            {
                case HallRequestDto hallRequestDto:
                    hallDetails = hallRequestDto.HallDetails;
                    seats = hallRequestDto.Seats.ToList();
                    break;
                case UpdateHallRequestDto updateHallRequestDto:
                    //hallDetails = updateHallRequestDto.HallDetails;
                    seats = updateHallRequestDto.Seats.ToList();
                    break;
            }
            var hallEntity = base.MapAsEntity(requestDto);
            if(hallDetails != null)
            {
                hallEntity.HallDetails = hallDetails.AsEntity<HallDetails>();
                hallEntity.HallDetails!.NumberOfSeats = hallEntity.Seats.Count;
                hallEntity.HallDetails!.MaxNumberOfSeats = hallEntity.HallDetails!.MaxNumberOfSeatsRows * hallEntity.HallDetails!.MaxNumberOfSeatsColumns;
                var area = hallDetails.TotalLength * hallDetails.TotalWidth;
                hallEntity.HallDetails!.TotalArea = Math.Round(area, 2);
            }

            hallEntity.Seats = seats.Select(seat =>
            {
                var seatEntity = seat.AsEntity<Seat>();
                return seatEntity;
            }).ToList();
            return hallEntity;
        }

        private IEntity MapToEntity(HallRent_HallRequestDto requestDto, Hall hallEntity)
        {
            //var requestSeats = requestDto.Seats.ToList();   
            if(hallEntity.HallDetails != null)
                hallEntity.HallDetails.NumberOfSeats = requestDto.Seats.Count;

            //var hallDetails = (HallDetails)requestDto.HallDetails.MapTo(oldEntity.HallDetails!);
            //var hallEntity = (Hall)requestDto.MapTo(oldEntity);
            // hallEntity.HallDetails = hallDetails;

            //hallEntity.HallDetails!.NumberOfSeats = hallEntity.Seats.Count;
            //hallEntity.HallDetails!.MaxNumberOfSeats = hallEntity.HallDetails!.MaxNumberOfSeatsRows * hallEntity.HallDetails!.MaxNumberOfSeatsColumns;

            hallEntity.Seats = MapSeats(hallEntity.Seats, requestDto.Seats);

            return hallEntity;
        }


        private IEntity MapToEntity(EventHallRequestDto requestDto, Hall oldEntity)
        {         
            var hallEntity = (Hall)requestDto.MapTo(oldEntity);

            var seatsWithoutReservations = oldEntity.Seats.Where(s => !s.Reservations.Any());
            var seatsWithoutReservationsNumbers = seatsWithoutReservations.Select(s => s.SeatNr);
            var requestSeatsWithoutReservations = requestDto.Seats.Where(s => seatsWithoutReservationsNumbers.Contains(s.SeatNr));

            oldEntity.Seats.ToList().RemoveAll(s => seatsWithoutReservations.Contains(s));
            var newHallSeatsWithoutReservations = MapSeats(oldEntity.Seats, requestSeatsWithoutReservations.ToList());
            oldEntity.Seats.ToList().AddRange(newHallSeatsWithoutReservations);


            /* hallEntity.HallDetails = (HallDetails)requestDto.HallDetails.MapTo(oldEntity.HallDetails!);

             hallEntity.HallDetails!.NumberOfSeats = hallEntity.Seats.Count;
             hallEntity.HallDetails!.MaxNumberOfSeats = hallEntity.HallDetails!.MaxNumberOfSeatsRows * hallEntity.HallDetails!.MaxNumberOfSeatsColumns;
             var area = requestDto!.HallDetails.TotalLength * requestDto!.HallDetails.TotalWidth;
             hallEntity.HallDetails!.TotalArea = Math.Round(area, 2);*/

            hallEntity.Seats = MapSeats(oldEntity.Seats, requestDto.Seats);

            return hallEntity;
        }


        protected sealed override IEntity MapToEntity(UpdateHallRequestDto requestDto, Hall oldEntity)
        {
            var hallEntity = (Hall)requestDto.MapTo(oldEntity);
           /* hallEntity.HallDetails = (HallDetails)requestDto.HallDetails.MapTo(oldEntity.HallDetails!);

            hallEntity.HallDetails!.NumberOfSeats = hallEntity.Seats.Count;
            hallEntity.HallDetails!.MaxNumberOfSeats = hallEntity.HallDetails!.MaxNumberOfSeatsRows * hallEntity.HallDetails!.MaxNumberOfSeatsColumns;
            var area = requestDto!.HallDetails.TotalLength * requestDto!.HallDetails.TotalWidth;
            hallEntity.HallDetails!.TotalArea = Math.Round(area, 2);*/

            hallEntity.Seats = MapSeats(oldEntity.Seats, requestDto.Seats);

            return hallEntity;
        }
    }
}
