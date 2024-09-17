
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Helpers;
using EventFlowAPI.Logic.Identity.Services.Interfaces;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces;
using EventFlowAPI.Logic.Services.CRUDServices.Services.BaseServices;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces;
using EventFlowAPI.Logic.UnitOfWork;

namespace EventFlowAPI.Logic.Services.CRUDServices.Services
{
    public sealed class ReservationService(
        IUnitOfWork unitOfWork,
        ISeatService seatService,
        IAuthService authService,
        ITicketCreatorService ticketCreator,
        IPdfBuilderService pdfBuilderService,
        IEmailSenderService emailSender) :
        GenericService<
            Reservation,
            ReservationRequestDto,
            ReservationResponseDto
        >(unitOfWork),
        IReservationService
    {
        private readonly ISeatService _seatService = seatService;
        private readonly IAuthService _authService = authService;
        private readonly ITicketCreatorService _ticketCreator = ticketCreator;
        private readonly IPdfBuilderService _pdfBuilderService = pdfBuilderService;
        private readonly IEmailSenderService _emailSender = emailSender;

        public async Task<Result<ReservationResponseDto>> MakeReservation(ReservationRequestDto? requestDto)
        {
            if (requestDto == null)
            {
                return Result<ReservationResponseDto>.Failure(Error.NullParameter);
            }
            var validationError = await ValidateEntity(requestDto);
            if(validationError != Error.None)
            {
                return Result<ReservationResponseDto>.Failure(validationError);
            }

            var seatsList = (await _unitOfWork.GetRepository<Seat>().GetAllAsync(q =>
                q.Where(s => requestDto.SeatsIds.Contains(s.Id)))).ToList();

            /*var currentUserIdResult = await _authService.GetCurrentUserId();
            if (!currentUserIdResult.IsSuccessful)
            {
                return Result<ReservationResponseDto>.Failure(currentUserIdResult.Error);
            }*/

            var ticketRepository = _unitOfWork.GetRepository<Ticket>();
            var ticket = await ticketRepository.GetOneAsync(requestDto.TicketId);
            var festival = ticket!.Festival;



            // If it is festival then additional payment couldn't be calculated
            List<byte[]> ticketBitmaps = [];

            if (festival != null)
            {
                // ticket for festival
                List<Reservation> reservationsList = [];

                var tickets = await ticketRepository.GetAllAsync(q => q.Where(t => t.FestivalId == festival.Id));

                Dictionary<Ticket, List<Seat>> eventSeatsDict = [];

                foreach(var eventTicket in tickets)
                {
                    var seatsInEventHall = seatsList.Where(s => s.HallId == eventTicket.Event.HallId).ToList();
                    eventSeatsDict.Add(eventTicket, seatsInEventHall);
                }

                var festivalGuid = Guid.NewGuid();

                foreach(var (eventTicket, seats) in eventSeatsDict)
                {
                    var reservationEntity = new Reservation
                    {
                        Id = 123123, // to delete
                        ReservationGuid = festivalGuid,
                        ReservationDate = DateTime.Now,
                        StartOfReservationDate = eventTicket.Event.StartDate,
                        EndOfReservationDate = eventTicket.Event.EndDate,
                        PaymentDate = DateTime.Now,
                        PaymentAmount = Math.Round(eventTicket.Price, 2),
                        TotalAddtionalPaymentPercentage = 0m,
                        TotalAdditionalPaymentAmount = 0m,
                        //UserId = currentUserIdResult.Value,
                        //PaymentTypeId = requestDto.PaymentTypeId,
                        //TicketId = eventTicket.Id,
                        PaymentType = new PaymentType
                        {
                            Name = "Płatność kartą"
                        },
                        User = new User // to delete
                        {
                            Name = "Mateusz",
                            Surname = "Strapczuk"
                        },
                        Ticket = eventTicket,
                        Seats = seats,
                    };
                    reservationsList.Add(reservationEntity);
                }

                ticketBitmaps = await _ticketCreator.CreateFestivalTicketPNG(festival, reservationsList);
                var ticketPDF = await _pdfBuilderService.CreateTicketPdf(reservationsList.First(), ticketBitmaps);
                
                var emailDto = new EmailDto
                {
                    Email = "mateusz.strapczuk@gmail.com",
                    Subject = $"Twoje bilety EventFlow - zamówienie nr {reservationsList.First().Id}",
                    Body = $"Test",
                    Attachments =
                    {
                        new AttachmentDto
                        {
                            Type = "application/pdf",
                            FileName =$"eventflow_bilety_{reservationsList.First().Id}.pdf",
                            Data = ticketPDF
                        }
                    }
                };
                await _emailSender.SendEmailAsync(emailDto);

                // Add Reservation to DB and generate Ticket 

            }
            else
            {
                // ticket for event 
                decimal paymentAmount = 0;
                decimal totalAdditionalPayment = 0;
                foreach (var seat in seatsList)
                {
                    var additionalPayment = ticket.Price * (seat.SeatType.AddtionalPaymentPercentage / 100);
                    totalAdditionalPayment += additionalPayment;
                    paymentAmount += ticket.Price + additionalPayment;
                }
                decimal totalAdditionalPaymentPercentage = Math.Round((totalAdditionalPayment / ticket.Price) * 100, 2);

                var reservationEntity = new Reservation
                {
                    Id = 12342, // to delete
                    ReservationGuid = Guid.NewGuid(),
                    ReservationDate = DateTime.Now,
                    StartOfReservationDate = ticket.Event.StartDate,
                    EndOfReservationDate = ticket.Event.EndDate,
                    PaymentDate = DateTime.Now,
                    TotalAddtionalPaymentPercentage = totalAdditionalPaymentPercentage,
                    TotalAdditionalPaymentAmount = Math.Round(totalAdditionalPayment, 2),
                    PaymentAmount = Math.Round(paymentAmount, 2),
                    //UserId = currentUserIdResult.Value,
                    //PaymentTypeId = requestDto.PaymentTypeId,
                    // TicketId = ticket.Id,  
                    PaymentType = new PaymentType
                    {
                        Name = "Płatność kartą"
                    },
                    User = new User // to delete
                    {
                        Name = "Mateusz",
                        Surname = "Strapczuk"
                    },
                    Ticket = ticket,
                    Seats = seatsList,
                };

                var ticketBitmap = await _ticketCreator.CreateEventTicketJPEG(reservationEntity);
                ticketBitmaps.Add(ticketBitmap);
                var ticketPDF = await _pdfBuilderService.CreateTicketPdf(reservationEntity, ticketBitmaps);
                var emailDto = new EmailDto
                {
                    Email = "mateusz.strapczuk@gmail.com",
                    Subject = $"Twoje bilety EventFlow - zamówienie nr {reservationEntity.Id}",
                    Body = $"Test",
                    Attachments =
                    {
                        new AttachmentDto
                        {
                            Type = "application/pdf",
                            FileName =$"eventflow_bilety_{reservationEntity.Id}.pdf",
                            Data = ticketPDF
                        }
                    }
                };
                await _emailSender.SendEmailAsync(emailDto);

                // Add Reservation to DB and generate Ticket 
            }

            return Result<ReservationResponseDto>.Success();
        }

        protected sealed override async Task<Error> ValidateEntity(ReservationRequestDto? requestDto, int? id = null)
        {
            if (requestDto == null)
            {
                return Error.NullParameter;
            }

            if (!await IsEntityExistInDB<PaymentType>(requestDto!.PaymentTypeId))
            {
                return PaymentTypeError.PaymentTypeNotFound;
            }
            if (!await IsEntityExistInDB<Ticket>(requestDto!.TicketId))
            {
                return TicketError.TicketNotFound;
            }

            var ticket = await _unitOfWork.GetRepository<Ticket>().GetOneAsync(requestDto.TicketId);
            
            List<Event> eventList = [];

            if(ticket!.Festival == null)
            {
                eventList.Add(ticket.Event);
            }
            else
            {
                eventList.AddRange(ticket!.Festival.Events);
            }

            var seats = await _unitOfWork.GetRepository<Seat>().GetAllAsync(q =>
                q.Where(s => requestDto.SeatsIds.Contains(s.Id)));

            if(seats.Count() != requestDto.SeatsIds.Count)
            {
                return SeatError.SeatNotFound;
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

        protected sealed override Task<bool> IsSameEntityExistInDatabase(ReservationRequestDto entityDto, int? id = null)
        {
            // is same reservation exist in db - reservation with reserved seat

            throw new NotImplementedException();
        }
    }
}
