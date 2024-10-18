using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Helpers;
using EventFlowAPI.Logic.Helpers.Enums;

namespace EventFlowAPI.Logic.Services.OtherServices.Interfaces
{
    public interface IEmailSenderService
    {
        Task<Error> SendInfo<TEntity>(TEntity entity, EmailType emailType, string userEmail, byte[]? attachmentData = null);
        /*Task SendEventPassRenewPDFAsync(EventPass eventPass, byte[] eventPassPDF);      
        Task SendInfoAboutCanceledEventPass(EventPass eventPass);
        Task SendEventPassPDFAsync(EventPass eventPass, byte[] eventPassPDF);
    
        Task SendTicketPDFAsync(Reservation reservation, byte[] ticketPDF);
        Task SendInfoAboutCanceledReservation(Reservation reservation);

        Task SendHallRentPDFAsync(HallRent hallRent, byte[] hallRentPDF, string fileName);*/

        Task SendEmailAsync(EmailDto emailDto);
        Task SendUpdatedTicketsAsync(List<(Reservation, byte[])> tupleList, OldEventInfo oldEventInfo);
        Task SendUpdatedHallRentsAsync(List<(HallRent, byte[])> tupleList, Hall oldHall);
        Task SendInfoAboutCanceledHallRents(List<HallRent> hallRentsToDelete);
        Task SendInfoAboutCanceledEvents(List<(Reservation, bool)> deleteReservationsInfo, Event? eventEntity = null);
    }
}
