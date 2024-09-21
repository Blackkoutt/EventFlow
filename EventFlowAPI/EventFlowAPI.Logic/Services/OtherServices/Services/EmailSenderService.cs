using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Helpers;
using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces;
using EventFlowAPI.Logic.Templates;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace EventFlowAPI.Logic.Services.OtherServices.Services
{
    public class EmailSenderService(
        IConfiguration configuration,
        IHtmlRendererService htmlRenderer,
        IAssetService assetService
        ) : IEmailSenderService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IHtmlRendererService _htmlRenderer = htmlRenderer;
        private readonly IAssetService _assetService = assetService;
        private string TicketMailSubject => $"Twoje bilety EventFlow - zamówienie nr {{0}}";
        private string CancelReservationMailSubject => $"Anulowanie zamówienia EventFlow - zamówienie nr {{0}}";
        private string TicketMailPDFFileName => $"eventflow_bilety_{{0}}.pdf";
        private string LogoContentId => "logoImage";


        public Task SendEmailAsync(EmailDto emailDto)
        {
            var client = GetSmtpClient();

            var mailMessage = new MailMessage(
                from: _configuration.GetSection("SmtpClient")["Email"]!,
                to: emailDto.Email,
                subject: emailDto.Subject,
                body: emailDto.Body
            );
            mailMessage.IsBodyHtml = emailDto.IsBodyHTML;

            if (mailMessage.IsBodyHtml)
            {
                AddAlternateView(mailMessage, ContentType.HTML, emailDto.LinkedResources);
            }

            if (emailDto.Attachments != null && emailDto.Attachments.Any())
            {
                AddAttachments(mailMessage, emailDto.Attachments);
            }
            return client.SendMailAsync(mailMessage);
        }


        public async Task SendInfoAboutCanceledReservation(Reservation reservation)
        {
            var paramDictionary = GetDefaultHTMLParams(reservation.Id);
            var htmlStringEmailBody = await _htmlRenderer.RenderHtmlToStringAsync<ReservationCancelEmailBody>(paramDictionary);
            var logoPath = _assetService.GetAssetPath(AssetType.Pictures, Picture.EventFlowLogo_Small.ToString());

            var emailDto = new EmailDto
            {
                Email = reservation.User.Email!,
                Subject =  string.Format(CancelReservationMailSubject, reservation.Id),
                Body = htmlStringEmailBody,
                IsBodyHTML = true,
                LinkedResources =
                {
                    new LinkedResource(logoPath, ContentType.PNG)
                    {
                        ContentId = LogoContentId,
                    }
                },
            };

            await SendEmailAsync(emailDto);
        }


        public async Task SendTicketPDFAsync(Reservation reservation, byte[] ticketPDF)
        {
            var paramDictionary = GetDefaultHTMLParams(reservation.Id);
            var htmlStringEmailBody = await _htmlRenderer.RenderHtmlToStringAsync<TicketEmailBody>(paramDictionary);
            var logoPath = _assetService.GetAssetPath(AssetType.Pictures, Picture.EventFlowLogo_Small.ToString()); 

            var emailDto = new EmailDto
            {
                Email = reservation.User.Email!,
                Subject = string.Format(TicketMailSubject, reservation.Id),
                Body = htmlStringEmailBody,
                IsBodyHTML = true,
                LinkedResources =
                {
                    new LinkedResource(logoPath, ContentType.PNG)
                    {
                        ContentId = LogoContentId,
                    }
                },
                Attachments =
                {
                    new AttachmentDto
                    {
                        Type = ContentType.PDF,
                        FileName = string.Format(TicketMailPDFFileName, reservation.Id),
                        Data = ticketPDF
                    }
                }
            };

            await SendEmailAsync(emailDto);
        }


        private Dictionary<string, object?> GetDefaultHTMLParams(int reservationId)
        {
            return new Dictionary<string, object?>()
            {
                { "ReservationId", reservationId },
                { "LogoContentId", LogoContentId }
            };
        }


        private SmtpClient GetSmtpClient()
        {         
            var from = _configuration.GetSection("SmtpClient")["Email"]!; //"eventflow.powiadomienia@gmail.com";
            var password = _configuration.GetSection("SmtpClient")["AppPasswd"]!; //"aamo yzzw onlg bvqw";
            return new SmtpClient()
            {
                Host = _configuration.GetSection("SmtpClient")["Host"]!,
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential(from, password)
            };
        }


        private void AddAttachments(MailMessage mailMessage, List<AttachmentDto> attachments)
        {
            foreach(AttachmentDto attachmentDto in attachments)
            {
                var stream = new MemoryStream(attachmentDto.Data);
                var attachment = new Attachment(stream, attachmentDto.FileName, attachmentDto.Type); // "application/pdf"
                mailMessage.Attachments.Add(attachment);
            }
        }


        private void AddAlternateView(MailMessage mailMessage, string viewType, List<LinkedResource> resources)
        {
            var alternateView = AlternateView.CreateAlternateViewFromString(mailMessage.Body, null, viewType);
            foreach (var resource in resources)
            {
                alternateView.LinkedResources.Add(resource);
            }
            mailMessage.AlternateViews.Add(alternateView);
        }
    }
}
