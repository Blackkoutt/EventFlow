﻿
using EventFlowAPI.DB.Entities;
using EventFlowAPI.DB.Entities.Abstract;
using EventFlowAPI.Logic.DTO.Interfaces;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.DTO.UpdateRequestDto;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Extensions;
using EventFlowAPI.Logic.Helpers;
using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Helpers.PayU;
using EventFlowAPI.Logic.Identity.Helpers;
using EventFlowAPI.Logic.Identity.Services.Interfaces;
using EventFlowAPI.Logic.Mapper.Extensions;
using EventFlowAPI.Logic.Query;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces;
using EventFlowAPI.Logic.Services.CRUDServices.Services.BaseServices;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces;
using EventFlowAPI.Logic.Services.OtherServices.Services;
using EventFlowAPI.Logic.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.HttpSys;
using Serilog;
using System.Linq;
using System.Text.Json;

namespace EventFlowAPI.Logic.Services.CRUDServices.Services
{
    public sealed class ReservationService(
        IUnitOfWork unitOfWork,
        ISeatService seatService,
        IAuthService authService,
        IFileService fileService,
        IEmailSenderService emailSender,
        IPaymentService paymentService,
        IHttpContextAccessor httpContextAccessor
        ) :
        GenericService<
            Reservation,
            ReservationRequestDto,
            UpdateReservationRequestDto,
            ReservationResponseDto,
            ReservationQuery
        >(unitOfWork, authService),
        IReservationService
    {
        private readonly ISeatService _seatService = seatService;
        private readonly IFileService _fileService = fileService;
        private readonly IEmailSenderService _emailSender = emailSender;
        private readonly IPaymentService _paymentService = paymentService;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly ITicketRepository _ticketRepository = (ITicketRepository)unitOfWork.GetRepository<Ticket>();

        public sealed override async Task<Result<IEnumerable<ReservationResponseDto>>> GetAllAsync(ReservationQuery query)
        {
            UserResponseDto? user = default!;
            if (query.GetAll != true)
            {
                var userResult = await _authService.GetCurrentUser();
                if (!userResult.IsSuccessful)
                    return Result<IEnumerable<ReservationResponseDto>>.Failure(userResult.Error);
                user = userResult.Value;
            }


            if (query.GetAll == true || user.IsInRole(Roles.Admin))
            {
                var allReservations = await _repository.GetAllAsync(q =>
                                                q.ByQuery(query)
                                                .GetPage(query.PageNumber, query.PageSize));

                var allReservationsDto = MapAsDto(allReservations);
                return Result<IEnumerable<ReservationResponseDto>>.Success(allReservationsDto);
            }
            else if (user.IsInRole(Roles.User))
            {
                var userReservations = await _repository.GetAllAsync(q =>
                                            q.ByQuery(query)
                                            .Where(r => r.User.Id == user.Id)
                                            .GetPage(query.PageNumber, query.PageSize));

                var userReservationsResponse = MapAsDto(userReservations);
                return Result<IEnumerable<ReservationResponseDto>>.Success(userReservationsResponse);
            }
            else
                return Result<IEnumerable<ReservationResponseDto>>.Failure(AuthError.UserDoesNotHaveSpecificRole);
        }


        public sealed override async Task<Result<ReservationResponseDto>> GetOneAsync(int id)
        {
            var reservationResult = await base.GetOneAsync(id);
            if(!reservationResult.IsSuccessful)
                return Result<ReservationResponseDto>.Failure(reservationResult.Error);

            var userResult = await _authService.GetCurrentUser();
            if (!userResult.IsSuccessful)
                return Result<ReservationResponseDto>.Failure(userResult.Error);

            var user = userResult.Value;
            var reservation = reservationResult.Value;
            var premissionError = CheckUserPremission(user, reservation.User!.Id);
            if (premissionError != Error.None)
                return Result<ReservationResponseDto>.Failure(premissionError);

            return Result<ReservationResponseDto>.Success(reservation);         
        }

        public async Task<Result<PayUCreatePaymentResponseDto>> CreateReservationPayment(ReservationRequestDto? requestDto)
        {

            var validationError = await ValidateEntity(requestDto, null);
            if (validationError != Error.None)
                return Result<PayUCreatePaymentResponseDto>.Failure(validationError);

            var userResult = await _authService.GetCurrentUser();
            if (!userResult.IsSuccessful)
                return Result<PayUCreatePaymentResponseDto>.Failure(userResult.Error);
            var user = userResult.Value;

            var ticket = await ((ITicketRepository)_unitOfWork.GetRepository<Ticket>()).GetOneAsync(requestDto!.TicketId);
            var seatsList = (await _seatService.GetSeatsByListOfIds(requestDto!.SeatsIds)).ToList();

            decimal reservationPrice = 0;
            if (requestDto!.IsReservationForFestival)
            {
                reservationPrice = ticket!.Price;
            }
            else
            {
                decimal totalAdditionalPayment = 0;
                foreach (var seat in seatsList)
                {
                    var additionalPayment = ticket!.Price * (seat.SeatType.AddtionalPaymentPercentage / 100);
                    totalAdditionalPayment += additionalPayment;
                    reservationPrice += ticket.Price + additionalPayment;
                }
            }

            
            string continuteUrl = string.Empty;
            if (requestDto.IsReservationForFestival) continuteUrl = $"http://localhost:5173/festivals/{ticket!.FestivalId}?reservation";
            else continuteUrl = $"http://localhost:5173/events/{ticket!.EventId}?reservation";

            var paymentRequest = new PayURequestPaymentDto
            {
                Description = $"Kupno biletu EventFlow",
                ContinueUrl = continuteUrl,
                TotalAmount = (int)(reservationPrice * 100),
                Products = new List<PayUProductDto>()
                {
                    new PayUProductDto
                    {
                        Name = $"Bilet EventFlow - {(requestDto.IsReservationForFestival ? ticket!.Festival!.Name : ticket!.Event.Name)}",
                        Price = (int)(reservationPrice * 100),
                        Quanitity = 1,
                    }
                },
                Buyer = new PayUBuyerDto
                {
                    Email = user.EmailAddress,
                    FirstName = user.Name,
                    LastName = user.Surname,
                }
            };

            var createPaymentResult = await _paymentService.CreatePayment(paymentRequest);
            if (!createPaymentResult.IsSuccessful)
                return Result<PayUCreatePaymentResponseDto>.Failure(createPaymentResult.Error);

            var response = createPaymentResult.Value;
            _httpContextAccessor.HttpContext!.Session.SetString("TransactionId", response.OrderId);
            _httpContextAccessor.HttpContext!.Session.SetString("ReservationRequest", JsonSerializer.Serialize(requestDto));

            return Result<PayUCreatePaymentResponseDto>.Success(response);
        }

        public async Task<Result<IEnumerable<ReservationResponseDto>>> MakeReservation(ReservationRequestDto? reservationRequestDto)
        {
            var requestDto = reservationRequestDto;
            if (requestDto == null)
            {
                var transactionStatusResult = await _paymentService.CheckNewTransactionStatus<ReservationRequestDto>("ReservationRequest");
                if (!transactionStatusResult.IsSuccessful)
                    return Result<IEnumerable<ReservationResponseDto>>.Failure(transactionStatusResult.Error);

                requestDto = transactionStatusResult.Value;
            }

            // Validation
            var validationError = await ValidateEntity(requestDto);
            if (validationError != Error.None)
                return Result<IEnumerable<ReservationResponseDto>>.Failure(validationError);

            // User
            var userResult = await _authService.GetCurrentUser();
            if (!userResult.IsSuccessful)
                return Result<IEnumerable<ReservationResponseDto>>.Failure(userResult.Error);

            var user = userResult.Value;

            // Event Pass
            var userActiveEventPass = (await _unitOfWork.GetRepository<EventPass>().GetAllAsync(q =>
                                            q.Where(ep =>
                                            ep.UserId == user.Id &&
                                            !ep.IsDeleted &&
                                            ep.EndDate > DateTime.Now))).FirstOrDefault();

            var seatsList = (await _seatService.GetSeatsByListOfIds(requestDto!.SeatsIds)).ToList();
            var ticketRepository = (ITicketRepository)_unitOfWork.GetRepository<Ticket>();
            var ticket = await ticketRepository.GetOneAsync(requestDto.TicketId);

            // if ticket is deleted
            var festival = ticket!.Festival;
            List<Reservation> reservationsList = [];
            var eventSeatsDict = await GetListOfSeatsForEachEventTicket(festival, seatsList);

            var paymentType = await _unitOfWork.GetRepository<PaymentType>().GetOneAsync(requestDto!.PaymentTypeId);

            if (festival != null && requestDto.IsReservationForFestival)
            {
                // Reservation for festival
                var reservations = CreateReservationEntitiesForFestival(
                     eventSeatsDict: eventSeatsDict!,
                     userId: user.Id,
                     paymentTypeId: requestDto.PaymentTypeId,
                     eventPass: (paymentType!.Name.ToLower() == "karnet" ? userActiveEventPass : null));


                reservationsList.AddRange(reservations);
            }
            else
            {
                // Reservation for event
                var reservationEntity = CreateReservationEntityForEvent(
                    ticket: ticket,
                    seatsList: seatsList,
                    userId: user.Id,
                    paymentTypeId: requestDto.PaymentTypeId,
                    eventPass: (paymentType!.Name.ToLower() == "karnet" ? userActiveEventPass : null));

                reservationsList.Add(reservationEntity);
            }

            // Add reservation to db
            var responseDtoList = await AddAsync(reservationsList);


            // Create and send Tickets
            var eventTicketCreateAndSendError = await CreateTicketAndSendByMailAsync(reservationsList, festival, user, isUpdate: false);

            if (eventTicketCreateAndSendError != Error.None)
                return Result<IEnumerable<ReservationResponseDto>>.Failure(eventTicketCreateAndSendError);


            return Result<IEnumerable<ReservationResponseDto>>.Success(responseDtoList);
        }



        // Soft delete reservation, hard delete ticket jpg and pdf from tables and blobStorage
        public sealed override async Task<Result<object>> DeleteAsync(int id)
        {
            var validationResult = await ValidateBeforeDelete(id);
            if (!validationResult.IsSuccessful)
                return Result<object>.Failure(validationResult.Error);

            var reservation = validationResult.Value.Entity;
            
            var user = validationResult.Value.User;

            var deleteError = await SoftDeleteReservationAndFileTickets(reservation, deleteForFestival: true);
            if (deleteError != Error.None)
                return Result<object>.Failure(deleteError);


            await _unitOfWork.SaveChangesAsync();
            //await _emailSender.SendInfoAboutCanceledReservation(reservation);

            // Send info about canceled reservations
            Log.Information($"{user.EmailAddress}");
            var sendError = await _emailSender.SendInfo(reservation, EmailType.Cancel, reservation.User.Email!);
            if (sendError != Error.None)
                return Result<object>.Failure(sendError);

            return Result<object>.Success();
        }


        private async Task<Error> UpdateTicketAndSendByMailAsync<TEntity>(List<Reservation> userReservations, TEntity? oldEntity = null, TEntity? newEntity = null) where TEntity : class
        {
            if (userReservations.Count == 0)
                return ReservationError.ReservationListIsEmpty;

            List<(Reservation, byte[])> userUpdatedTicketsPDFs = [];

            var reservationList = userReservations.DistinctBy(r => r.ReservationGuid);

            foreach (var userReservation in reservationList)
            {
                var festivalId = userReservation.Ticket.FestivalId ?? -1;
                var festival = await _unitOfWork.GetRepository<Festival>().GetOneAsync(festivalId);
                var reservationsList = new List<Reservation> { userReservation };
                var festivalReservations = (await _repository.GetAllAsync(q =>
                                            q.Where(r =>
                                            r.Id != userReservation.Id &&
                                            r.UserId == userReservation.UserId &&
                                            r.Ticket.FestivalId == festivalId))).ToList();
                reservationsList.AddRange(festivalReservations);

                // Create Ticket JPGs
                var ticketJPGFilesResult = await _fileService.CreateTicketJPGBitmapsAndEntities(festival, reservationsList, isUpdate: true);
                if (!ticketJPGFilesResult.IsSuccessful)
                    return ticketJPGFilesResult.Error;

                var ticketJPGBitmaps = ticketJPGFilesResult.Value.Bitmaps;

                Reservation reservationEntity = userReservations.First();

                // Create Ticket PDF 
                var ticketPDFFileResult = await _fileService.CreateTicketPDFBitmapAndEntity(reservationEntity, ticketJPGBitmaps, isUpdate: true);
                if (!ticketPDFFileResult.IsSuccessful)
                    return ticketPDFFileResult.Error;

                userUpdatedTicketsPDFs.Add((userReservation, ticketPDFFileResult.Value.Bitmap));
            }
            // Send e-mail with updated tickets
            await _emailSender.SendUpdatedTicketsAsync(userUpdatedTicketsPDFs, oldEntity, newEntity);

            return Error.None;
        }

        public async Task<Error> SendMailsAboutUpdatedReservations<TEntity>(IEnumerable<Reservation> reservationsForEvent, TEntity? oldEntity = null, TEntity? newEntity = null) where TEntity : class
        {
            var reservationsGroupByUser = reservationsForEvent.GroupBy(r => r.UserId);

            // send mail about updated event 
            foreach (var group in reservationsGroupByUser)
            {
                var userReservations = group.ToList();
                var updateAndSendError = await UpdateTicketAndSendByMailAsync(userReservations, oldEntity, newEntity);
                if(updateAndSendError != Error.None)
                    return updateAndSendError;
            }
            return Error.None;
        }


        public async Task<Error> CancelReservationsInCauseOfDeleteEventOrHallOrFestival(IEnumerable<Reservation> reservations, Event? eventEntity = null, Festival? festival = null)
        {
            var reservationsGroupByUser = reservations.GroupBy(r => r.UserId)
                                            .Select(g => new
                                            {
                                                UserId = g.Key,
                                                ReservationList = g.ToList(),
                                            });
 
            List<(Reservation, bool)> deleteReservationsInfo = [];
            foreach (var group in reservationsGroupByUser)
            {
                deleteReservationsInfo.Clear();
                foreach (var reservation in group.ReservationList)
                {
                    bool deleteFestival = false;
                    var eventsInFestival = await _repository
                                                    .GetAllAsync(q => q.Where(r =>
                                                    r.IsFestivalReservation &&
                                                    r.ReservationGuid == reservation.ReservationGuid &&
                                                    !r.IsDeleted));

                    if (reservation.IsFestivalReservation && eventsInFestival.Count() <= 2)
                        deleteFestival = true;

                    if(!deleteReservationsInfo.Select(r => r.Item1.ReservationGuid).Contains(reservation.ReservationGuid))
                    {
                        var deleteEventReservationsError = await SoftDeleteReservationAndFileTickets(reservation, deleteForFestival: deleteFestival);
                        if (deleteEventReservationsError != Error.None)
                            return deleteEventReservationsError;
                    }

                    deleteReservationsInfo.Add((reservation, deleteFestival));
                }
                await _emailSender.SendInfoAboutCanceledEvents(deleteReservationsInfo, eventEntity, festival);
            }
            return Error.None;
        }



        public async Task<Error> SoftDeleteReservationAndFileTickets(Reservation reservation, bool deleteForFestival = false)
        {
            if (reservation.IsFestivalReservation && deleteForFestival)
            {
                var allReservationsWithSameGuid = await _repository.GetAllAsync(q =>
                                               q.Where(r => r.ReservationGuid == reservation.ReservationGuid));

                foreach (var res in allReservationsWithSameGuid)
                {
                    SoftDeleteReservation(res);
                }
                var ticketFileDeleteError = await HardDeleteTicketFiles(reservation.Id);
                if (ticketFileDeleteError != Error.None)
                    return ticketFileDeleteError;
            }
            else
            {
                SoftDeleteReservation(reservation);

                if (!reservation.IsFestivalReservation)
                {
                    var ticketFileDeleteError = await HardDeleteTicketFiles(reservation.Id);
                    if (ticketFileDeleteError != Error.None)
                        return ticketFileDeleteError;
                }
            }

            return Error.None;
        }
        private void SoftDeleteReservation(Reservation reservation)
        {
            reservation.IsDeleted = true;
            reservation.DeleteDate = DateTime.Now;
            _repository.Update(reservation);
        }

        private async Task<Error> HardDeleteTicketFiles(int reservationId)
        {
            var ticketJPGRepository = (ITicketJPGRepository)_unitOfWork.GetRepository<TicketJPG>();
            var ticketJPGList = await ticketJPGRepository.GetAllJPGsForReservation(reservationId);
            var deleteTicketJPGsError = await _fileService.DeleteFileEntities(ticketJPGList);
            if (deleteTicketJPGsError != Error.None)
                return deleteTicketJPGsError;

            var ticketPDFRepository = (ITicketPDFRepository)_unitOfWork.GetRepository<TicketPDF>();
            var ticketPDFList = await ticketPDFRepository.GetAllPDFsForReservation(reservationId);
            var deleteTicketPDFError = await _fileService.DeleteFileEntities(ticketPDFList);
            if (deleteTicketPDFError != Error.None)
                return deleteTicketPDFError;

            return Error.None;
        }


        private async Task<IEnumerable<ReservationResponseDto>> AddAsync(List<Reservation> reservationsList)
        {
            foreach (var reservation in reservationsList)
            {
                await _repository.AddAsync(reservation);
            }

            await _unitOfWork.SaveChangesAsync();

            List<ReservationResponseDto> responseDtoList = [];
            foreach (var reservation in reservationsList)
            {
                var response = MapAsDto(reservation);
                responseDtoList.Add(response);
            }

            return responseDtoList;
        }


        private async Task<Error> CreateTicketAndSendByMailAsync(List<Reservation> reservationsList, Festival? festival, UserResponseDto user, bool isUpdate = false)
        {
            if (reservationsList.Count == 0)
                return ReservationError.ReservationListIsEmpty;

            Reservation reservation = reservationsList.First();

            // Create Ticket JPGs
            var ticketJPGFilesResult = await _fileService.CreateTicketJPGBitmapsAndEntities(festival, reservationsList, isUpdate);
            if (!ticketJPGFilesResult.IsSuccessful)
                return ticketJPGFilesResult.Error;

            var ticketJPGBitmaps = ticketJPGFilesResult.Value.Bitmaps;
            var ticketJPGEntityList = ticketJPGFilesResult.Value.FileEntities;

            // Create Ticket PDF 
            var ticketPDFFileResult = await _fileService.CreateTicketPDFBitmapAndEntity(reservation, ticketJPGBitmaps, isUpdate);
            if (!ticketPDFFileResult.IsSuccessful)
                return ticketPDFFileResult.Error;

            var ticketPDFBitmap = ticketPDFFileResult.Value.Bitmap;
            var ticketPDFEntityList = new List<IFileEntity>() { ticketPDFFileResult.Value.FileEntity };

            // Update local database
            await UpdateReservationsAfterCreatingFile(ticketJPGEntityList, reservationsList, ContentType.JPEG);
            await UpdateReservationsAfterCreatingFile(ticketPDFEntityList, reservationsList, ContentType.PDF);

            // Send email with pdf
            //await _emailSender.SendTicketPDFAsync(reservationEntity, ticketPDFBitmap);

            // Send tickets via email
            var sendError = await _emailSender.SendInfo(reservation, EmailType.Create, user.EmailAddress, attachmentData: ticketPDFBitmap);
            if (sendError != Error.None)
                return sendError;

            return Error.None;
        }

        private async Task UpdateReservationsAfterCreatingFile(List<IFileEntity> fileEntityList, List<Reservation> reservationsList, string contentType)
        {
            foreach (var reservation in reservationsList)
            {
                if (contentType == ContentType.PDF)
                {
                    reservation.TicketPDF = (TicketPDF)fileEntityList.First();
                }
                else if (contentType == ContentType.JPEG)
                {
                    reservation.TicketsJPG = fileEntityList.Select(file => (TicketJPG)file).ToList();
                }

                _repository.Update(reservation);
            }
            await _unitOfWork.SaveChangesAsync();
        }


        private Reservation CreateReservationEntityForEvent(Ticket ticket, List<Seat> seatsList,
            string userId, int paymentTypeId, EventPass? eventPass = null)
        {
            var ticketPaymentResult = CalculateEventTicketPayment(ticket, seatsList);
            decimal AddPaymentPercent = ticketPaymentResult.AddPaymentPercent;
            decimal TotalAddPayment = ticketPaymentResult.TotalAddPayment;
            decimal TotalPayment = ticketPaymentResult.TotalPayment;
            decimal TotalDiscount = 0;


            int? eventPassId = eventPass?.Id;
            if (eventPassId != null)
            {
                TotalDiscount = TotalPayment;
                TotalPayment = 0;
            }

            return new Reservation
            {
                ReservationGuid = Guid.NewGuid(),
                IsFestivalReservation = false,
                ReservationDate = DateTime.Now,
                StartDate = ticket.Event.StartDate,
                EndDate = ticket.Event.EndDate,
                PaymentDate = DateTime.Now,
                TotalAddtionalPaymentPercentage = AddPaymentPercent,
                TotalAdditionalPaymentAmount = Math.Round(TotalAddPayment, 2),
                PaymentAmount = Math.Round(TotalPayment, 2),
                TotalDiscount = TotalDiscount,
                UserId = userId,
                PaymentTypeId = paymentTypeId,
                TicketId = ticket.Id,
                Ticket = ticket,
                Seats = seatsList,
                EventPassId = eventPassId
            };
        }

        private List<Reservation> CreateReservationEntitiesForFestival(Dictionary<Ticket, List<Seat>> eventSeatsDict, 
            string userId, int paymentTypeId, EventPass? eventPass = null)
        {
            List<Reservation> reservationEntitites = [];

            var festivalGuid = Guid.NewGuid();

            int? eventPassId = eventPass?.Id;
            decimal TotalPayment = 0;
            decimal TotalDiscount = 0;

            foreach (var (eventTicket, seats) in eventSeatsDict)
            {
                TotalPayment = eventTicket.Price;
                TotalDiscount = 0;
                if (eventPassId != null)
                {
                    TotalDiscount = TotalPayment;
                    TotalPayment = 0;
                }
                var reservationEntity = new Reservation
                {
                    ReservationGuid = festivalGuid,
                    IsFestivalReservation = true,
                    ReservationDate = DateTime.Now,
                    StartDate = eventTicket.Event.StartDate,
                    EndDate = eventTicket.Event.EndDate,
                    PaymentDate = DateTime.Now,
                    PaymentAmount = Math.Round(TotalPayment, 2),
                    TotalAddtionalPaymentPercentage = 0m,
                    TotalAdditionalPaymentAmount = 0m,
                    TotalDiscount = TotalDiscount,
                    UserId = userId,
                    PaymentTypeId = paymentTypeId,
                    TicketId = eventTicket.Id,
                    Ticket = eventTicket,
                    Seats = seats,
                    EventPassId = eventPassId,
                };
                reservationEntitites.Add(reservationEntity);
            }
            return reservationEntitites;
        }


        private (decimal AddPaymentPercent, decimal TotalAddPayment, decimal TotalPayment) CalculateEventTicketPayment(
            Ticket ticket, List<Seat> seatsList)
        {
            decimal paymentAmount = 0;
            decimal totalAdditionalPayment = 0;
            foreach (var seat in seatsList)
            {
                var additionalPayment = ticket.Price * (seat.SeatType.AddtionalPaymentPercentage / 100);
                totalAdditionalPayment += additionalPayment;
                paymentAmount += ticket.Price + additionalPayment;
            }
            decimal totalAdditionalPaymentPercentage = Math.Round((totalAdditionalPayment / ticket.Price) * 100, 2);

            return (
                AddPaymentPercent: totalAdditionalPaymentPercentage,
                TotalAddPayment: totalAdditionalPayment,
                TotalPayment: paymentAmount
            );
        }


        private async Task<Dictionary<Ticket, List<Seat>>?> GetListOfSeatsForEachEventTicket(Festival? festival, List<Seat> seatsList)
        {
            if (festival == null) return null;

            var ticketsForFestival = await _ticketRepository.GetAllAsync(q => q.Where(t => 
                                                                    t.FestivalId == festival.Id));

            Dictionary<Ticket, List<Seat>> eventSeatsDict = [];
            foreach (var eventTicket in ticketsForFestival)
            {
                var seatsInEventHall = seatsList.Where(s => s.HallId == eventTicket.Event.HallId).ToList();
                eventSeatsDict.Add(eventTicket, seatsInEventHall);
            }
            return eventSeatsDict;
        }


        private List<Event> GetTicketEventList(Ticket ticket)
        {
            List<Event> eventList = [];

            if (ticket!.Festival == null)
            {
                eventList.Add(ticket.Event);
            }
            else
            {
                eventList.AddRange(ticket!.Festival.Events);
            }
            return eventList;
        }

        public async Task<IEnumerable<Reservation>> GetActiveReservationsForEvent(int eventId)
        {
            return await _repository
                        .GetAllAsync(q => q.Where(r =>
                        !r.IsDeleted &&
                        r.EndDate > DateTime.Now &&
                        r.Ticket.EventId == eventId));
        }
        public async Task<IEnumerable<Reservation>> GetActiveReservationsForFestival(int festivalId)
        {
            return await _repository
                        .GetAllAsync(q => q.Where(r =>
                        !r.IsDeleted &&
                        r.EndDate > DateTime.Now &&
                        r.IsFestivalReservation && 
                        r.Ticket.FestivalId == festivalId));
        }


        private async Task<Error> ValidateSeats(List<int> seatIds, IEnumerable<Seat> seats, List<Event> eventList)
        {
            foreach(var seatId in seatIds)
            {
                if (!await IsEntityExistInDB<Seat>(seatId))
                    return SeatError.SeatNotFound;
            }
            if (seatIds.Distinct().Count() != seats.Count())
                return SeatError.SeatsDuplicate;

            if (seats.Count() != seatIds.Count)
                return SeatError.SeatNotFound;


            int correctSeatsCount = 0;
            foreach (var eventEntity in eventList)
            {
                foreach (var seat in seats)
                {
                    if (eventEntity.Hall.Seats.Any(s => s.Id == seat.Id)) correctSeatsCount++;
                    if (_seatService.IsSeatHaveActiveReservationForEvent(seat, eventEntity))
                        return SeatError.SeatNotAvailable;
                }
            }
            if (correctSeatsCount != seats.Count())
                return SeatError.SeatsAreNotInGivenEventHall;

            return Error.None;
        }

        private async Task<Error> ValidateEventPass(int paymentTypeId, List<int> seatIds, bool isReservationForFestival , UserResponseDto user,
            Festival? festivalEntity, Event eventEntity, List<Seat> seats, Ticket ticket)
        {
            var paymentType = await _unitOfWork.GetRepository<PaymentType>().GetOneAsync(paymentTypeId);
            var userActiveEventPass = (await _unitOfWork.GetRepository<EventPass>().GetAllAsync(q =>
                                           q.Where(ep =>
                                           ep.UserId == user.Id &&
                                           !ep.IsDeleted &&
                                           ep.EndDate > DateTime.Now))).FirstOrDefault();

            if (paymentType!.Name.ToLower() == "karnet")
            {
                if (userActiveEventPass == null)
                    return EventPassError.UserDoesNotHaveActiveEventPass;
                if ((festivalEntity != null && userActiveEventPass.EndDate < festivalEntity.EndDate) || (userActiveEventPass.EndDate < eventEntity.EndDate))
                    return EventPassError.EventPassExpireBeforeEndOfEvent;

                if (!isReservationForFestival && seatIds.Count > 1)
                    return EventPassError.OnlyOneSeatPerEvent;

                var activeReservationsForEventPass = await _repository.GetAllAsync(q =>
                                                    q.Where(r =>
                                                    r.EventPassId == userActiveEventPass.Id &&
                                                    !r.IsDeleted &&
                                                    r.EndDate > DateTime.Now));

                var isAnyActiveReservationForEvent = activeReservationsForEventPass.Any(r => r.Ticket.EventId == ticket.EventId);
                if (isAnyActiveReservationForEvent)
                    return ReservationError.CannotMakeReservationForSameEventByEventPass;

                var eventSeatsDict = await GetListOfSeatsForEachEventTicket(festivalEntity, seats);
                if (isReservationForFestival && eventSeatsDict != null)
                {
                    foreach ((var ticketKey, var seatsValue) in eventSeatsDict!)
                    {
                        if (seatsValue.Count > 1)
                            return EventPassError.OnlyOneSeatPerEvent;
                    }
                }
            }
            return Error.None;
        }

        protected override async Task<Result<(Reservation Entity, UserResponseDto User)>> ValidateBeforeDelete(int id)
        {
            if (id < 0)
                return Result<(Reservation, UserResponseDto)>.Failure(Error.RouteParamOutOfRange);

            var reservation = await _repository.GetOneAsync(id);
            if (reservation == null)
                return Result<(Reservation, UserResponseDto)>.Failure(ReservationError.ReservationDoesNotExist);

            if (reservation.IsDeleted)
                return Result<(Reservation, UserResponseDto)>.Failure(ReservationError.ReservationIsDeleted);

            if (reservation.IsExpired)
                return Result<(Reservation, UserResponseDto)>.Failure(ReservationError.ReservationIsExpired);

            var userResult = await _authService.GetCurrentUser();
            if (!userResult.IsSuccessful)
                return Result<(Reservation, UserResponseDto)>.Failure(userResult.Error);

            var user = userResult.Value;

            var premissionError = CheckUserPremission(user, reservation.User.Id);
            if (premissionError != Error.None)
                return Result<(Reservation, UserResponseDto)>.Failure(premissionError);

            return Result<(Reservation, UserResponseDto)>.Success((reservation, user));
        }


        protected sealed override async Task<Error> ValidateEntity(IRequestDto? requestDto, int? id = null)
        {
            if (id != null && id < 0)
                return Error.RouteParamOutOfRange;

            if (requestDto == null)
                return Error.NullParameter;

            int paymentTypeId;
            int ticketId;
            bool isReservationForFestival;
            List<int> seatIds = [];
            switch (requestDto)
            {
                case ReservationRequestDto reservationRequestDto:
                    paymentTypeId = reservationRequestDto.PaymentTypeId;
                    ticketId = reservationRequestDto.TicketId;
                    isReservationForFestival = reservationRequestDto.IsReservationForFestival;
                    seatIds = reservationRequestDto.SeatsIds;
                    break;
                case UpdateReservationRequestDto updateReservationRequestDto:
                    paymentTypeId = updateReservationRequestDto.PaymentTypeId;
                    ticketId = updateReservationRequestDto.TicketId;
                    isReservationForFestival = updateReservationRequestDto.IsReservationForFestival;
                    seatIds = updateReservationRequestDto.SeatsIds;
                    break;
                default:
                    return Error.BadRequestType;
            }


            if (!await IsEntityExistInDB<PaymentType>(paymentTypeId))
                return PaymentTypeError.PaymentTypeNotFound;

            var ticket = await _ticketRepository.GetOneAsync(ticketId);
            if (ticket is null)
                return TicketError.TicketNotFound;

            var userResult = await _authService.GetCurrentUser();
            if (!userResult.IsSuccessful)
                return userResult.Error;

            Festival? Festival = ticket.Festival;
            Event Event = ticket.Event;
            if (isReservationForFestival && Festival == null)
                return ReservationError.CannotMakeReservationForFestivalOnEventTicket;

            var eventList = GetTicketEventList(ticket);
            foreach(var eventEntity in eventList)
            {
                if(eventEntity.StartDate < DateTime.Now)
                    return ReservationError.EventIsOutOfDate;
            }

            var seats = await _seatService.GetSeatsByListOfIds(seatIds);
            var seatsError = await ValidateSeats(seatIds, seats, eventList);
            if(seatsError != Error.None)
                return seatsError;

            var eventPassError = await ValidateEventPass(paymentTypeId, seatIds, isReservationForFestival,
                                                    userResult.Value, Festival, Event, seats.ToList(), ticket);
            if(eventPassError != Error.None)
                return eventPassError;

            return Error.None;
        }

        protected sealed override IEnumerable<ReservationResponseDto> MapAsDto(IEnumerable<Reservation> records)
        {
            return records.Select(entity =>
            {
                var responseDto = entity.AsDto<ReservationResponseDto>();
                responseDto.User = entity.User.AsDto<UserResponseDto>();
                responseDto.ReservationGuid = entity.ReservationGuid;
                responseDto.User.EmailAddress = entity.User.Email!;
                responseDto.User.UserData = null;
                responseDto.PaymentType = entity.PaymentType.AsDto<PaymentTypeResponseDto>();
                responseDto.Ticket = entity.Ticket.AsDto<TicketResponseDto>();
                responseDto.ReservationStatus = GetEntityStatus(entity);
                responseDto.Ticket.Event!.Details = null;
                responseDto.Ticket.Event.Tickets = [];
                responseDto.Ticket.Event.Hall!.Seats = [];
                responseDto.Ticket.Event.Hall!.HallDetails = null;
                if (responseDto.Ticket.Festival is not null)
                {
                    responseDto.Ticket.Festival.Details = null;
                    responseDto.Ticket.Festival.Events = [];
                    responseDto.Ticket.Festival.MediaPatrons = [];
                    responseDto.Ticket.Festival.Sponsors = [];
                    responseDto.Ticket.Festival.Organizers = [];
                }
                responseDto.Seats = entity.Seats.Select(seat =>
                {
                    return seat.AsDto<SeatResponseDto>();
                }).ToList();
                return responseDto;
            });
        }


        protected sealed override ReservationResponseDto MapAsDto(Reservation entity)
        {
            var responseDto = entity.AsDto<ReservationResponseDto>();
            responseDto.User = entity.User.AsDto<UserResponseDto>();
            responseDto.User.EmailAddress = entity.User.Email!;
            responseDto.User.UserData = null;
            responseDto.ReservationGuid = entity.ReservationGuid;
            responseDto.PaymentType = entity.PaymentType.AsDto<PaymentTypeResponseDto>();
            responseDto.Ticket = entity.Ticket.AsDto<TicketResponseDto>();
            responseDto.ReservationStatus = GetEntityStatus(entity);
            responseDto.Ticket.Event!.Details = null;
            responseDto.Ticket.Event.Tickets = [];
            responseDto.Ticket.Event.Hall!.Seats = [];
            responseDto.Ticket.Event.Hall!.HallDetails = null;
            if (responseDto.Ticket.Festival is not null)
            {
                responseDto.Ticket.Festival.Details = null;
                responseDto.Ticket.Festival.Events = [];
                responseDto.Ticket.Festival.MediaPatrons = [];
                responseDto.Ticket.Festival.Sponsors = [];
                responseDto.Ticket.Festival.Organizers = [];
            }
            responseDto.Seats = entity.Seats.Select(seat =>
            {
                return seat.AsDto<SeatResponseDto>();
            }).ToList();

            return responseDto;
        }


        // Not used
        protected sealed override async Task<Result<bool>> IsSameEntityExistInDatabase(IRequestDto requestDto, int? id = null)
        {
            // is same reservation exist in db - reservation with reserved seat

            throw new NotImplementedException();
        }
    }
}
