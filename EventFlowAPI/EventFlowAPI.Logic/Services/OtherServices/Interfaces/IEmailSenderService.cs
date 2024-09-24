using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Helpers;

namespace EventFlowAPI.Logic.Services.OtherServices.Interfaces
{
    public interface IEmailSenderService
    {
        Task SendEventPassRenewPDFAsync(EventPass eventPass, byte[] eventPassPDF);
        Task SendInfoAboutCanceledEventPass(EventPass eventPass);
        Task SendEventPassPDFAsync(EventPass eventPass, byte[] eventPassPDF);
        Task SendEmailAsync(EmailDto emailDto);
        Task SendTicketPDFAsync(Reservation reservation, byte[] ticketPDF);
        Task SendInfoAboutCanceledReservation(Reservation reservation);
    }
}
