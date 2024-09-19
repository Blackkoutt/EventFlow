
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Mapper.Extensions;
using EventFlowAPI.Logic.Repositories.Interfaces;
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
        IUserService userService,
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
        private readonly IUserService _userService = userService;
        private readonly ITicketCreatorService _ticketCreator = ticketCreator;
        private readonly IPdfBuilderService _pdfBuilderService = pdfBuilderService;
        private readonly IEmailSenderService _emailSender = emailSender;
        private readonly ITicketRepository _ticketRepository = (ITicketRepository)unitOfWork.GetRepository<Ticket>();

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
                                        

        public async Task<Result<IEnumerable<ReservationResponseDto>>> MakeReservation(ReservationRequestDto? requestDto)
        {
            // Validation
            if (requestDto == null)
            {
                return Result<IEnumerable<ReservationResponseDto>>.Failure(Error.NullParameter);
            }
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
            var seatsList = (await _seatService.GetSeatsByListOfIds(requestDto.SeatsIds)).ToList();
            var ticketRepository = (ITicketRepository)_unitOfWork.GetRepository<Ticket>();
            var ticket = await ticketRepository.GetOneAsync(requestDto.TicketId);
            var festival = ticket!.Festival;
            List<Reservation> reservationsList = [];

            if (festival != null)
            {
                // Reservation for festival
                reservationsList = await CreateReservationEntitiesForFestival(
                                        festival: festival,
                                        seatsList: seatsList,
                                        userId: user.Id,
                                        paymentTypeId: requestDto.PaymentTypeId);
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



        private async Task<Error> CreateTicketAndSendByMailAsync(List<Reservation> reservationsList, Festival? festival)
        {
            if (reservationsList.Count == 0)
            {
                return ReservationError.ReservationListIsEmpty;
            }

            List<byte[]> ticketBitmaps = [];
            Reservation reservationEntity = reservationsList.First();

            if (festival is not null)
            {
                ticketBitmaps = await _ticketCreator.CreateFestivalTicketPNG(festival, reservationsList);
            }
            else
            {
                var ticketBitmap = await _ticketCreator.CreateEventTicketJPEG(reservationEntity);
                ticketBitmaps.Add(ticketBitmap);
            }

            var ticketPDF = await _pdfBuilderService.CreateTicketPdf(reservationEntity, ticketBitmaps);
            await _emailSender.SendTicketPDFAsync(reservationEntity, ticketPDF);

            return Error.None;
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
