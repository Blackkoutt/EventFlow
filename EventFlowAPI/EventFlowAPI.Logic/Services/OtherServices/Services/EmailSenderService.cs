﻿using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Helpers;
using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces;
using EventFlowAPI.Logic.Templates;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace EventFlowAPI.Logic.Services.OtherServices.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IConfiguration _configuration;
        private readonly IHtmlRendererService _htmlRenderer;
        private readonly IAssetService _assetService;
        private readonly string logoPath;
        private LinkedResource logoResource;


        public EmailSenderService(IConfiguration configuration, IHtmlRendererService htmlRenderer, IAssetService assetService)
        {
            _configuration = configuration;
            _htmlRenderer = htmlRenderer;
            _assetService = assetService;
            logoPath = _assetService.GetAssetPath(AssetType.Pictures, Picture.EventFlowLogo_Small.ToString());
            logoResource = new LinkedResource(logoPath, ContentType.PNG)
            {
                ContentId = LogoContentId,
            };
        }


        // Ticket
        private string TicketMailSubject => $"Twoje bilety EventFlow - rezerwacja nr {{0}}";
        private string CancelReservationMailSubject => $"Anulowanie rezerwacji EventFlow - rezerwacja nr {{0}}";
        private string TicketMailPDFFileName => $"eventflow_bilet_{{0}}.pdf";

        // Event Pass
        private string EventPassMailSubject => $"Twój karnet EventFlow - karnet nr {{0}}";
        private string EventPassRenewMailSubject => $"Przedłużenie karnetu EventFlow - karnet nr {{0}}";
        private string CancelEventPassMailSubject => $"Anulowanie karnetu EventFlow - karnet nr {{0}}";
        private string EventPassMailPDFFileName => $"eventflow_karnet_{{0}}.pdf";
        private string LogoContentId => "logoImage";
       


        // EventPass
        public async Task SendEventPassRenewPDFAsync(EventPass eventPass, byte[] eventPassPDF)
        {
            var paramDictionary = GetEventPassHTMLParams(eventPass.Id);
            var htmlStringEmailBody = await _htmlRenderer.RenderHtmlToStringAsync<EventPassRenew>(paramDictionary);

            var emailDto = new EmailDto
            {
                Email = eventPass.User.Email!,
                Subject = string.Format(EventPassRenewMailSubject, eventPass.Id),
                Body = htmlStringEmailBody,
                IsBodyHTML = true,
                LinkedResources = { logoResource },
                Attachments =
                {
                    new AttachmentDto
                    {
                        Type = ContentType.PDF,
                        FileName = string.Format(EventPassMailPDFFileName, eventPass.Id),
                        Data = eventPassPDF
                    }
                }
            };

            await SendEmailAsync(emailDto);
        }

        public async Task SendEventPassPDFAsync(EventPass eventPass, byte[] eventPassPDF)
        {
            var paramDictionary = GetEventPassHTMLParams(eventPass.Id);
            var htmlStringEmailBody = await _htmlRenderer.RenderHtmlToStringAsync<EventPassEmailBody>(paramDictionary);

            var emailDto = new EmailDto
            {
                Email = eventPass.User.Email!,
                Subject = string.Format(EventPassMailSubject, eventPass.Id),
                Body = htmlStringEmailBody,
                IsBodyHTML = true,
                LinkedResources = { logoResource },
                Attachments =
                {
                    new AttachmentDto
                    {
                        Type = ContentType.PDF,
                        FileName = string.Format(EventPassMailPDFFileName, eventPass.Id),
                        Data = eventPassPDF
                    }
                }
            };

            await SendEmailAsync(emailDto);
        }

        public async Task SendInfoAboutCanceledEventPass(EventPass eventPass)
        {
            var paramDictionary = GetEventPassHTMLParams(eventPass.Id);
            var htmlStringEmailBody = await _htmlRenderer.RenderHtmlToStringAsync<EventPassCancelEmailBody>(paramDictionary);

            var emailDto = new EmailDto
            {
                Email = eventPass.User.Email!,
                Subject = string.Format(CancelEventPassMailSubject, eventPass.Id),
                Body = htmlStringEmailBody,
                IsBodyHTML = true,
                LinkedResources = { logoResource }
            };

            await SendEmailAsync(emailDto);
        }


        // Reservation
        public async Task SendInfoAboutCanceledReservation(Reservation reservation)
        {
            var paramDictionary = GetTicketHTMLParams(reservation.Id);
            var htmlStringEmailBody = await _htmlRenderer.RenderHtmlToStringAsync<ReservationCancelEmailBody>(paramDictionary);

            var emailDto = new EmailDto
            {
                Email = reservation.User.Email!,
                Subject =  string.Format(CancelReservationMailSubject, reservation.Id),
                Body = htmlStringEmailBody,
                IsBodyHTML = true,
                LinkedResources = { logoResource }
            };

            await SendEmailAsync(emailDto);
        }


        public async Task SendTicketPDFAsync(Reservation reservation, byte[] ticketPDF)
        {
            var paramDictionary = GetTicketHTMLParams(reservation.Id);
            var htmlStringEmailBody = await _htmlRenderer.RenderHtmlToStringAsync<TicketEmailBody>(paramDictionary);

            var emailDto = new EmailDto
            {
                Email = reservation.User.Email!,
                Subject = string.Format(TicketMailSubject, reservation.Id),
                Body = htmlStringEmailBody,
                IsBodyHTML = true,
                LinkedResources = { logoResource },
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


        private Dictionary<string, object?> GetEventPassHTMLParams(int eventPassId)
        {
            return new Dictionary<string, object?>()
            {
                { "EventPassId", eventPassId },
                { "LogoContentId", LogoContentId }
            };
        }

        private Dictionary<string, object?> GetTicketHTMLParams(int reservationId)
        {
            return new Dictionary<string, object?>()
            {
                { "ReservationId", reservationId },
                { "LogoContentId", LogoContentId }
            };
        }
    }
}
