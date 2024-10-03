using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Helpers;

namespace EventFlowAPI.Logic.Services.OtherServices.Interfaces
{
    public interface IEmailSenderService
    {
        Task SendEventPassRenewPDFAsync(EventPass eventPass, byte[] eventPassPDF);
        Task SendUpdatedTicketsAsync(List<(Reservation, byte[])> tupleList, OldEventInfo oldEventInfo);
        Task SendInfoAboutCanceledEventPass(EventPass eventPass);
        Task SendEventPassPDFAsync(EventPass eventPass, byte[] eventPassPDF);
        Task SendEmailAsync(EmailDto emailDto);
        Task SendTicketPDFAsync(Reservation reservation, byte[] ticketPDF);
        Task SendInfoAboutCanceledReservation(Reservation reservation);
        Task SendInfoAboutCanceledEvents(List<Reservation> reservationList, Event? eventEntity = null);
    }
}
