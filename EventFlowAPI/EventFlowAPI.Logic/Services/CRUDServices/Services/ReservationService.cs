
using EventFlowAPI.DB.Entities;
using EventFlowAPI.DB.Entities.Abstract;
using EventFlowAPI.Logic.DTO.Interfaces;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Extensions;
using EventFlowAPI.Logic.Helpers;
using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Identity.Helpers;
using EventFlowAPI.Logic.Mapper.Extensions;
using EventFlowAPI.Logic.Query;
using EventFlowAPI.Logic.Query.Abstract;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces;
using EventFlowAPI.Logic.Services.CRUDServices.Services.BaseServices;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces;
using EventFlowAPI.Logic.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.Services.CRUDServices.Services
{
    public sealed class ReservationService(
        IUnitOfWork unitOfWork,
        ISeatService seatService,
        IUserService userService,
        IFileService fileService,
        IEmailSenderService emailSender
        ) :
        GenericService<
            Reservation,
            ReservationRequestDto,
            ReservationResponseDto
        >(unitOfWork),
        IReservationService
    {
        private readonly ISeatService _seatService = seatService;
        private readonly IUserService _userService = userService;
        private readonly IFileService _fileService = fileService;
        private readonly IEmailSenderService _emailSender = emailSender;
        private readonly ITicketRepository _ticketRepository = (ITicketRepository)unitOfWork.GetRepository<Ticket>();


        // Update Event or Festival => update Reservation (start and end of reservation), add updated reservation to list, invoke 
        // CreateTicketAndSendByMailAsync(List<Reservation> reservationsList, Festival? festival)

        /* public sealed override async Task<Result<ReservationResponseDto>> UpdateAsync(int id, ReservationRequestDto? requestDto)
         {
             var updateResult = await base.UpdateAsync(id, requestDto);
             if (!updateResult.IsSuccessful)
                 return Result<ReservationResponseDto>.Failure(updateResult.Error);

             var reservationResponse = updateResult.Value;
             var reservationList = (await _repository.GetAllAsync(q => 
                                         q.Where(r => r.ReservationGuid == reservationResponse.ReservationGuid))).ToList();
             var festival = reservationList.First().Ticket.Festival;

             var updateTicketAndSendMailError = await UpdateTicketAndSendByMailAsync(reservationList, festival);
             if (updateTicketAndSendMailError != Error.None)
                 return Result<ReservationResponseDto>.Failure(updateTicketAndSendMailError);


         }


         public async Task<Error> UpdateTicketAndSendByMailAsync(List<Reservation> reservationsList, Festival? festival)
         {
             if (reservationsList.Count == 0)
                 return ReservationError.ReservationListIsEmpty;

             Reservation reservationEntity = reservationsList.First();

             // Create Ticket JPGs
             var ticketJPGFilesResult = await _fileService.CreateTicketJPGBitmapsAndEntities(festival, reservationsList, isUpdate: true);
             if (!ticketJPGFilesResult.IsSuccessful)
                 return ticketJPGFilesResult.Error;

             var ticketJPGBitmaps = ticketJPGFilesResult.Value.Bitmaps;

             // Create Ticket PDF 
             var ticketPDFFileResult = await _fileService.CreateTicketPDFBitmapAndEntity(reservationEntity, ticketJPGBitmaps, isUpdate: true);
             if (ticketPDFFileResult.IsSuccessful)
                 return ticketPDFFileResult.Error;

             // NEED TO UPDATE DB
             // New method to send 
             await _emailSender.SendTicketPDFAsync(reservationEntity, ticketPDFBitmap);
             return Error.None;

         }*/


        public sealed override async Task<Result<IEnumerable<ReservationResponseDto>>> GetAllAsync(QueryObject query)
        {
            var resQuery = query as ReservationQuery;
            if (resQuery == null)
                return Result<IEnumerable<ReservationResponseDto>>.Failure(QueryError.BadQueryObject);

            var userResult = await _userService.GetCurrentUser();
            if (!userResult.IsSuccessful)
                return Result<IEnumerable<ReservationResponseDto>>.Failure(userResult.Error);

            var resRepo = ((IReservationRepository)_repository);

            var user = userResult.Value;
            if (user.IsInRole(Roles.Admin))
            {
                var allReservations = await _repository.GetAllAsync(q =>
                                                resRepo.ReservationsByStatus(q, resQuery.Status)
                                                .SortBy(resQuery.SortBy, resQuery.SortDirection));

                var allReservationsDto = MapAsDto(allReservations);
                return Result<IEnumerable<ReservationResponseDto>>.Success(allReservationsDto);
            }
            else if (user.IsInRole(Roles.User))
            {
                var userReservations = await _repository.GetAllAsync(q =>
                                            resRepo.ReservationsByStatus(q, resQuery.Status)
                                            .Where(r => r.User.Id == user.Id)
                                            .SortBy(resQuery.SortBy, resQuery.SortDirection));

                var userReservationsResponse = MapAsDto(userReservations);
                return Result<IEnumerable<ReservationResponseDto>>.Success(userReservationsResponse);
            }
            else
            {
                return Result<IEnumerable<ReservationResponseDto>>.Failure(AuthError.UserDoesNotHaveSpecificRole);
            }
        }


        public sealed override async Task<Result<ReservationResponseDto>> GetOneAsync(int id)
        {
            var reservationResult = await base.GetOneAsync(id);
            if(!reservationResult.IsSuccessful)
                return Result<ReservationResponseDto>.Failure(reservationResult.Error);

            var userResult = await _userService.GetCurrentUser();
            if (!userResult.IsSuccessful)
                return Result<ReservationResponseDto>.Failure(userResult.Error);

            var user = userResult.Value;
            var reservation = reservationResult.Value;
            var premissionError = CheckUserPremissionToReservation(user, reservation);
            if (premissionError != Error.None)
                return Result<ReservationResponseDto>.Failure(premissionError);

            return Result<ReservationResponseDto>.Success(reservation);         
        }

        // Soft delete reservation, hard delete ticket jpg and pdf from tables and blobStorage
        public sealed override async Task<Result<object>> DeleteAsync(int id)
        {
            if (id < 0)
                return Result<object>.Failure(Error.RouteParamOutOfRange);

            var reservation = await _repository.GetOneAsync(id);
            if (reservation == null)
                return Result<object>.Failure(Error.NotFound);

            if (reservation.IsCanceled)
                return Result<object>.Failure(ReservationError.ReservationDoesNotExist);

            if (!reservation.IsReservationActive)
                return Result<object>.Failure(ReservationError.ReservationIsExpired);

            var userResult = await _userService.GetCurrentUser();
            if (!userResult.IsSuccessful)
                return Result<object>.Failure(userResult.Error);

            var user = userResult.Value;

            var premissionError = CheckUserPremissionToReservation(user, MapAsDto(reservation));
            if (premissionError != Error.None)
                return Result<object>.Failure(premissionError);

            var allReservationsWithSameGuid = await _repository.GetAllAsync(q =>
                                                q.Where(r => r.ReservationGuid == reservation.ReservationGuid));

            foreach(var res in allReservationsWithSameGuid)
            {
                res.IsCanceled = true;
                _repository.Update(res);
            }
            //_repository.Delete(reservation);     

            var ticketJPGRepository = (ITicketJPGRepository)_unitOfWork.GetRepository<TicketJPG>();
            var ticketJPGList = await ticketJPGRepository.GetAllJPGsForReservation(id);
            var deleteTicketJPGsError = await _fileService.DeleteFileEntities(ticketJPGList);
            if(deleteTicketJPGsError != Error.None)
                return Result<object>.Failure(deleteTicketJPGsError);

            var ticketPDFRepository = (ITicketPDFRepository)_unitOfWork.GetRepository<TicketPDF>();
            var ticketPDFList = await ticketPDFRepository.GetAllPDFsForReservation(id);
            var deleteTicketPDFError = await _fileService.DeleteFileEntities(ticketPDFList);
            if (deleteTicketPDFError != Error.None)
                return Result<object>.Failure(deleteTicketPDFError);

            await _unitOfWork.SaveChangesAsync();

            await _emailSender.SendInfoAboutCanceledReservation(reservation);

            return Result<object>.Success();
        }


        public async Task<Result<IEnumerable<ReservationResponseDto>>> MakeReservation(ReservationRequestDto? requestDto)
        {
            // Validation
            var validationError = await ValidateEntity(requestDto);
            if(validationError != Error.None)
            {
                return Result<IEnumerable<ReservationResponseDto>>.Failure(validationError);
            }

            // User
            var userResult = await _userService.GetCurrentUser();
            if (!userResult.IsSuccessful)
            {
                return Result<IEnumerable<ReservationResponseDto>>.Failure(userResult.Error);    
            }

            var user = userResult.Value;
            var seatsList = (await _seatService.GetSeatsByListOfIds(requestDto!.SeatsIds)).ToList();
            var ticketRepository = (ITicketRepository)_unitOfWork.GetRepository<Ticket>();
            var ticket = await ticketRepository.GetOneAsync(requestDto.TicketId);
            var festival = ticket!.Festival;
            List<Reservation> reservationsList = [];

            if (festival != null)
            {
                // Reservation for festival
                var reservations = await CreateReservationEntitiesForFestival(
                                        festival: festival,
                                        seatsList: seatsList,
                                        userId: user.Id,
                                        paymentTypeId: requestDto.PaymentTypeId);

                reservationsList.AddRange(reservations);
            }
            else
            {
                // Reservation for event
                var reservationEntity = CreateReservationEntityForEvent(
                                            ticket: ticket,
                                            seatsList: seatsList,
                                            userId: user.Id,
                                            paymentTypeId: requestDto.PaymentTypeId);

                reservationsList.Add(reservationEntity);
            }

            // Add reservation to db
            var responseDtoList = await AddAsync(reservationsList);


            // Create and send Tickets
            var eventTicketCreateAndSendError = await CreateTicketAndSendByMailAsync(reservationsList, festival);

            if (eventTicketCreateAndSendError != Error.None)
            {
                return Result<IEnumerable<ReservationResponseDto>>.Failure(eventTicketCreateAndSendError);
            }

            return Result<IEnumerable<ReservationResponseDto>>.Success(responseDtoList);
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


        private async Task<Error> CreateTicketAndSendByMailAsync(List<Reservation> reservationsList, Festival? festival)
        {
            if (reservationsList.Count == 0)
                return ReservationError.ReservationListIsEmpty;

            Reservation reservationEntity = reservationsList.First();

            // Create Ticket JPGs
            var ticketJPGFilesResult = await _fileService.CreateTicketJPGBitmapsAndEntities(festival, reservationsList, isUpdate: false);
            if (!ticketJPGFilesResult.IsSuccessful)
                return ticketJPGFilesResult.Error;

            var ticketJPGBitmaps = ticketJPGFilesResult.Value.Bitmaps;
            var ticketJPGEntityList = ticketJPGFilesResult.Value.FileEntities;

            // Create Ticket PDF 
            var ticketPDFFileResult = await _fileService.CreateTicketPDFBitmapAndEntity(reservationEntity, ticketJPGBitmaps, isUpdate: false);
            if (!ticketPDFFileResult.IsSuccessful)
                return ticketPDFFileResult.Error;

            var ticketPDFBitmap = ticketPDFFileResult.Value.Bitmap;
            var ticketPDFEntityList = new List<IFileEntity>() { ticketPDFFileResult.Value.FileEntity };

            // Update local database
            await UpdateReservationsAfterCreatingFile(ticketJPGEntityList, reservationsList, ContentType.JPEG);
            await UpdateReservationsAfterCreatingFile(ticketPDFEntityList, reservationsList, ContentType.PDF);

            // Send email with pdf
            await _emailSender.SendTicketPDFAsync(reservationEntity, ticketPDFBitmap);

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

      

        protected sealed override IEnumerable<ReservationResponseDto> MapAsDto(IEnumerable<Reservation> records)
        {
            return records.Select(entity =>
            {
                var responseDto = entity.AsDto<ReservationResponseDto>();
                responseDto.User = entity.User.AsDto<UserResponseDto>();
                responseDto.User.UserData = null;
                responseDto.PaymentType = entity.PaymentType.AsDto<PaymentTypeResponseDto>();
                responseDto.Ticket = entity.Ticket.AsDto<TicketResponseDto>();
                responseDto.Ticket.Event.Details = null;
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
            responseDto.User.UserData = null;
            responseDto.PaymentType = entity.PaymentType.AsDto<PaymentTypeResponseDto>();
            responseDto.Ticket = entity.Ticket.AsDto<TicketResponseDto>();
            responseDto.Ticket.Event.Details = null;
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


        private Reservation CreateReservationEntityForEvent(Ticket ticket, List<Seat> seatsList, string userId, int paymentTypeId)
        {
            var ticketPaymentResult = CalculateEventTicketPayment(ticket, seatsList);

            return new Reservation
            {
                ReservationGuid = Guid.NewGuid(),
                ReservationDate = DateTime.Now,
                StartOfReservationDate = ticket.Event.StartDate,
                EndOfReservationDate = ticket.Event.EndDate,
                PaymentDate = DateTime.Now,
                TotalAddtionalPaymentPercentage = ticketPaymentResult.AddPaymentPercent,
                TotalAdditionalPaymentAmount = Math.Round(ticketPaymentResult.TotalAddPayment, 2),
                PaymentAmount = Math.Round(ticketPaymentResult.TotalPayment, 2),
                UserId = userId,
                PaymentTypeId = paymentTypeId,
                TicketId = ticket.Id,
                Ticket = ticket,
                Seats = seatsList,
            };
        }

        private async Task<List<Reservation>> CreateReservationEntitiesForFestival(Festival festival,
                List<Seat> seatsList, string userId, int paymentTypeId)
        {
            List<Reservation> reservationEntitites = [];

            var eventSeatsDict = await GetListOfSeatsForEachEventTicket(festival.Id, seatsList);
            var festivalGuid = Guid.NewGuid();

            foreach (var (eventTicket, seats) in eventSeatsDict)
            {
                var reservationEntity = new Reservation
                {
                    ReservationGuid = festivalGuid,
                    ReservationDate = DateTime.Now,
                    StartOfReservationDate = eventTicket.Event.StartDate,
                    EndOfReservationDate = eventTicket.Event.EndDate,
                    PaymentDate = DateTime.Now,
                    PaymentAmount = Math.Round(eventTicket.Price, 2),
                    TotalAddtionalPaymentPercentage = 0m,
                    TotalAdditionalPaymentAmount = 0m,
                    UserId = userId,
                    PaymentTypeId = paymentTypeId,
                    TicketId = eventTicket.Id,
                    Ticket = eventTicket,
                    Seats = seats,
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




        protected sealed override async Task<Error> ValidateEntity(ReservationRequestDto? requestDto, int? id = null)
        {
            if (id != null && id < 0)
            {
                return Error.RouteParamOutOfRange;
            }

            if (requestDto == null)
            {
                return Error.NullParameter;
            }

            if (!await IsEntityExistInDB<PaymentType>(requestDto!.PaymentTypeId))
            {
                return PaymentTypeError.PaymentTypeNotFound;
            }

            var ticket = await _ticketRepository.GetOneAsync(requestDto!.TicketId);
            if (ticket is null)
            {
                return TicketError.TicketNotFound;
            }

            var eventList = GetTicketEventList(ticket);
            foreach(var eventEntity in eventList)
            {
                if(eventEntity.StartDate < DateTime.Now)
                {
                    return ReservationError.EventIsOutOfDate;
                }
            }

            var seats = await _seatService.GetSeatsByListOfIds(requestDto.SeatsIds);

            if (seats.Count() != requestDto.SeatsIds.Count)
            {
                return SeatError.SeatNotFound;
            }
            if (seats.DistinctBy(seats => seats.Id).Count() != requestDto.SeatsIds.Count)
            {
                return SeatError.SeatsDuplicate;
            }

            foreach (var eventEntity in eventList)
            {
                foreach (var seat in seats)
                {
                    if (_seatService.IsSeatHaveActiveReservationForEvent(seat, eventEntity))
                    {
                        return SeatError.SeatNotAvailable;
                    }
                }
            }

            return Error.None;
        }

        private async Task<Dictionary<Ticket, List<Seat>>> GetListOfSeatsForEachEventTicket(int festivalId, List<Seat> seatsList)
        {
            var ticketsForFestival = await _ticketRepository.GetAllAsync(q => q.Where(t => t.FestivalId == festivalId));

            Dictionary<Ticket, List<Seat>> eventSeatsDict = [];
            foreach (var eventTicket in ticketsForFestival)
            {
                var seatsInEventHall = seatsList.Where(s => s.HallId == eventTicket.Event.HallId).ToList();
                eventSeatsDict.Add(eventTicket, seatsInEventHall);
            }
            return eventSeatsDict;
        }


        private Error CheckUserPremissionToReservation(UserResponseDto user, ReservationResponseDto reservation)
        {
            if (user.IsInRole(Roles.User))
            {
                if (reservation.User!.Id == user.Id)
                    return Error.None;
                else
                    return AuthError.UserDoesNotHavePremissionToResource;
            }
            else if (user.IsInRole(Roles.Admin))
            {
                return Error.None;
            }
            else
            {
                return AuthError.UserDoesNotHaveSpecificRole;
            }
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

        protected sealed override Task<bool> IsSameEntityExistInDatabase(ReservationRequestDto entityDto, int? id = null)
        {
            // is same reservation exist in db - reservation with reserved seat

            throw new NotImplementedException();
        }
    }
}
