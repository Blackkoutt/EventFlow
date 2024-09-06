
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Errors;
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
        ITicketCreatorService ticketCreator) :
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

            decimal paymentAmount = 0;

            foreach (var seat in seatsList)
            {
                var additionalPayment = ticket.Price * (seat.SeatType.AddtionalPaymentPercentage/100);
                paymentAmount += ticket.Price + additionalPayment;
            }

            if (festival != null)
            {
                // ticket for festival
                List<Reservation> reservationsList = [];

                var tickets = await ticketRepository.GetAllAsync(q => q.Where(t => t.FestivalId == festival.Id));

                Dictionary<Ticket, List<Seat>> eventSeatsDict = new Dictionary<Ticket, List<Seat>>();

                foreach(var eventTicket in tickets)
                {
                    var seatsInEventHall = seatsList.Where(s => s.HallId == eventTicket.Event.HallId).ToList();
                    eventSeatsDict.Add(eventTicket, seatsInEventHall);
                }

                foreach(var (eventTicket, seats) in eventSeatsDict)
                {
                    var reservationEntity = new Reservation
                    {
                        ReservationDate = DateTime.Now,
                        StartOfReservationDate = eventTicket.Event.StartDate,
                        EndOfReservationDate = eventTicket.Event.EndDate,
                        PaymentDate = DateTime.Now,
                        PaymentAmount = paymentAmount,
                        //UserId = currentUserIdResult.Value,
                        PaymentTypeId = requestDto.PaymentTypeId,
                        //TicketId = eventTicket.Id,
                        Ticket = eventTicket,
                        Seats = seats,
                    };
                    reservationsList.Add(reservationEntity);
                }

                // Add Reservation to DB and generate Ticket 

            }
            else
            {  
                // ticket for event 

                var reservationEntity = new Reservation
                {
                    ReservationDate = DateTime.Now,
                    StartOfReservationDate = ticket.Event.StartDate,
                    EndOfReservationDate = ticket.Event.EndDate,
                    PaymentDate = DateTime.Now,
                    PaymentAmount = paymentAmount,
                    //UserId = currentUserIdResult.Value,
                    PaymentTypeId = requestDto.PaymentTypeId,
                    // TicketId = ticket.Id,  
                    Ticket = ticket,
                    Seats = seatsList,
                };

                await _ticketCreator.CreateEventTicketJPG(reservationEntity);

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
