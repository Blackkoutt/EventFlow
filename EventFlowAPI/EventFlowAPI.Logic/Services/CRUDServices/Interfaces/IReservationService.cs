using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Helpers;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces.BaseInterfaces;

namespace EventFlowAPI.Logic.Services.CRUDServices.Interfaces
{
    public interface IReservationService :
        IGenericService<
            Reservation,
            ReservationRequestDto,
            ReservationResponseDto
        >
    {
        Task<Result<IEnumerable<ReservationResponseDto>>> MakeReservation(ReservationRequestDto? requestDto);
        Task<Error> UpdateTicketAndSendByMailAsync(List<Reservation> userReservations, OldEventInfo oldEventInfo);
        Task<IEnumerable<Reservation>> GetActiveReservationsForEvent(int eventId);
        Task<Error> SoftDeleteReservationAndFileTickets(Reservation reservation, bool deleteForFestival = false);
        Task SendMailsAboutUpdatedReservations(IEnumerable<Reservation> reservationsForEvent, OldEventInfo oldEventInfo);
        Task<Error> CancelReservationsInCauseOfDeleteEventOrHall(IEnumerable<Reservation> reservations, Event? eventEntity = null);
    }
}
