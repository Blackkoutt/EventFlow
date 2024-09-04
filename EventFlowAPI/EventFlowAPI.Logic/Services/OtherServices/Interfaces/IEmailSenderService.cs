using EventFlowAPI.Logic.Helpers;

namespace EventFlowAPI.Logic.Services.OtherServices.Interfaces
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(EmailDto emailDto);
    }
}
