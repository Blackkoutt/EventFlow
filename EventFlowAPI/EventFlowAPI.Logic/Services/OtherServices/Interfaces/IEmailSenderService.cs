using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Helpers;

namespace EventFlowAPI.Logic.Services.OtherServices.Interfaces
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(EmailDto emailDto);
        Task SendTicketPDFAsync(Reservation reservation, byte[] ticketPDF);
        Task SendInfoAboutCanceledReservation(Reservation reservation);
    }
}
