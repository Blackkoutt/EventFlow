using EventFlowAPI.Logic.Helpers;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace EventFlowAPI.Logic.Services.OtherServices.Services
{
    public class EmailSenderService(IConfiguration configuration) : IEmailSenderService
    {
        private readonly IConfiguration _configuration = configuration;
        public Task SendEmailAsync(EmailDto emailDto)
        {
            var client = GetSmtpClient();

            var mailMessage = new MailMessage(
                from: _configuration.GetSection("SmtpClient")["Email"]!,
                to: emailDto.Email,
                subject: emailDto.Subject,
                body: emailDto.Body
            );

            if (emailDto.Attachments != null && emailDto.Attachments.Any())
            {
                foreach(AttachmentDto attachmentDto in emailDto.Attachments)
                {
                    using (var stream = new MemoryStream(attachmentDto.Data))
                    {
                        var attachment = new Attachment(stream, attachmentDto.FileName, attachmentDto.Type); // "application/pdf"
                        mailMessage.Attachments.Add(attachment);
                    }
                }
            }
            return client.SendMailAsync(mailMessage);
        }
        private SmtpClient GetSmtpClient()
        {         
            var from = _configuration.GetSection("SmtpClient")["Email"]!;//"eventflow.powiadomienia@gmail.com";
            var password = _configuration.GetSection("SmtpClient")["AppPasswd"]!;//"aamo yzzw onlg bvqw";
            return new SmtpClient()
            {
                Host = _configuration.GetSection("SmtpClient")["Host"]!,
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential(from, password)
            };
        }
    }
}
