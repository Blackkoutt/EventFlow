﻿using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Errors;
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
        private string ReservationCreateMailSubject => $"Twoje bilety EventFlow - rezerwacja nr {{0}}";
        private string ReservationCancelMailSubject => $"Anulowanie rezerwacji EventFlow - rezerwacja nr {{0}}";     
        private string CancelEventMailSubject => $"Odwołanie wydarzenia - EventFlow";
        private string CancelEventsMailSubject => $"Odwołanie wydarzeń - EventFlow";
        private string UpdateTicketMailSubject => $"Zmiana organizacji wydarzenia {{0}}";
       


        // Event Pass
        private string EventPassCreateMailSubject => $"Twój karnet EventFlow - karnet nr {{0}}";
        private string EventPassRenewMailSubject => $"Przedłużenie karnetu EventFlow - karnet nr {{0}}";
        private string EventPassCancelMailSubject => $"Anulowanie karnetu EventFlow - karnet nr {{0}}";
        private string LogoContentId => "logoImage";


        // Hall Rent
        private string HallRentCreateMailSubject => $"Twoja rezerwacja sali EventFlow - rezerwacja nr {{0}}";
        private string HallRentCancelMailSubject => $"Anulowanie rezerwacji sali EventFlow - rezerwacja nr {{0}}";
        private string HallRentUpdateMailSubject => $"Zmiany w rezerwacjach sal EventFlow";
        private string HallRentsCancelMailSubject => $"Odwołanie rezerwacji sal - EventFlow";



        // PDF FileName
        private string ReservationPDFFileName => $"eventflow_bilet_{{0}}.pdf";
        private string EventPassPDFFileName => $"eventflow_karnet_{{0}}.pdf";
        private string HallRentPDFFileName => $"eventflow_wynajem_sali_{{0}}.pdf";


        // EventPass

        // no no no 
        /*public async Task SendEventPassRenewPDFAsync(EventPass eventPass, byte[] eventPassPDF)
        {
            var paramDictionary = GetDefaultHTMLParams(eventPass.Id);
            var htmlStringEmailBody = await _htmlRenderer.RenderHtmlToStringAsync<EventPassRenewEmailBody>(paramDictionary);

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
                        FileName = string.Format(EventPassPDFFileName, eventPass.Id),
                        Data = eventPassPDF
                    }
                }
            };

            await SendEmailAsync(emailDto);
        }

        // no no no
        public async Task SendEventPassPDFAsync(EventPass eventPass, byte[] eventPassPDF)
        {
            var paramDictionary = GetDefaultHTMLParams(eventPass.Id);
            var htmlStringEmailBody = await _htmlRenderer.RenderHtmlToStringAsync<EventPassEmailBody>(paramDictionary);

            var emailDto = new EmailDto
            {
                Email = eventPass.User.Email!,
                Subject = string.Format(EventPassCreateMailSubject, eventPass.Id),
                Body = htmlStringEmailBody,
                IsBodyHTML = true,
                LinkedResources = { logoResource },
                Attachments =
                {
                    new AttachmentDto
                    {
                        Type = ContentType.PDF,
                        FileName = string.Format(EventPassPDFFileName, eventPass.Id),
                        Data = eventPassPDF
                    }
                }
            };

            await SendEmailAsync(emailDto);
        }


        // no no no
        public async Task SendInfoAboutCanceledEventPass(EventPass eventPass)
        {
            var paramDictionary = GetDefaultHTMLParams(eventPass);
            var htmlStringEmailBody = await _htmlRenderer.RenderHtmlToStringAsync<EventPassCancelEmailBody>(paramDictionary);

            var emailDto = new EmailDto
            {
                Email = eventPass.User.Email!,
                Subject = string.Format(EventPassCancelMailSubject, eventPass.Id),
                Body = htmlStringEmailBody,
                IsBodyHTML = true,
                LinkedResources = { logoResource }
            };

            await SendEmailAsync(emailDto);
        }

        // no no no
        public async Task SendInfoAboutCanceledHallRent(HallRent hallRent)
        {
            var paramDictionary = GetDefaultHTMLParams(hallRent);
            var htmlStringEmailBody = await _htmlRenderer.RenderHtmlToStringAsync<HallRentCancelEmailBody>(paramDictionary);

            var emailDto = new EmailDto
            {
                Email = hallRent.User.Email!,
                Subject = string.Format(HallRentCancelMailSubject, hallRent.Id),
                Body = htmlStringEmailBody,
                IsBodyHTML = true,
                LinkedResources = { logoResource }
            };

            await SendEmailAsync(emailDto);
        }


        // no no no
        public async Task SendInfoAboutCanceledReservation(Reservation reservation)
        {
            var paramDictionary = GetDefaultHTMLParams(reservation);
            var htmlStringEmailBody = await _htmlRenderer.RenderHtmlToStringAsync<ReservationCancelEmailBody>(paramDictionary);

            var emailDto = new EmailDto
            {
                Email = reservation.User.Email!,
                Subject = string.Format(ReservationCancelMailSubject, reservation.Id),
                Body = htmlStringEmailBody,
                IsBodyHTML = true,
                LinkedResources = { logoResource }
            };

            await SendEmailAsync(emailDto);
        }

        // no no no
        public async Task SendHallRentPDFAsync(HallRent hallRent, byte[] hallRentPDF, string fileName)
        {
            var paramDictionary = GetDefaultHTMLParams(hallRent);
            var htmlStringEmailBody = await _htmlRenderer.RenderHtmlToStringAsync<HallRentCreateEmailBody>(paramDictionary);
            var emailDto = new EmailDto
            {
                Email = hallRent.User.Email!,
                Subject = string.Format(HallRentCreateMailSubject, hallRent.Id),
                Body = htmlStringEmailBody,
                IsBodyHTML = true,
                LinkedResources = { logoResource },
                Attachments =
                {
                    new AttachmentDto
                    {
                        Type = ContentType.PDF,
                        FileName = fileName,
                        Data = hallRentPDF
                    }
                }
            };
            await SendEmailAsync(emailDto);
        }

        // no no no
        public async Task SendTicketPDFAsync(Reservation reservation, byte[] ticketPDF)
        {
            var paramDictionary = GetDefaultHTMLParams(reservation);
            string htmlStringEmailBody = string.Empty;
            if (reservation.EventPass == null)
                htmlStringEmailBody = await _htmlRenderer.RenderHtmlToStringAsync<TicketEmailBody>(paramDictionary);
            else
                htmlStringEmailBody = await _htmlRenderer.RenderHtmlToStringAsync<TicketEventPassEmailBody>(paramDictionary);

            var emailDto = new EmailDto
            {
                Email = reservation.User.Email!,
                Subject = string.Format(ReservationCreateMailSubject, reservation.Id),
                Body = htmlStringEmailBody,
                IsBodyHTML = true,
                LinkedResources = { logoResource },
                Attachments =
                {
                    new AttachmentDto
                    {
                        Type = ContentType.PDF,
                        FileName = string.Format(ReservationPDFFileName, reservation.Id),
                        Data = ticketPDF
                    }
                }
            };

            await SendEmailAsync(emailDto);
        }*/


        public async Task<Error> SendInfo<TEntity>(TEntity entity, EmailType emailType, string userEmail, byte[]? attachmentData = null)
        {
            if (string.IsNullOrEmpty(userEmail))
                return MailSenderError.UserEmailIsNullOrEmpty;

            var emailBody = await GetRenderedBody(entity, emailType);

            var emailDto = new EmailDto
            {
                Email = userEmail,
                Subject = GetMailSubject(entity, emailType),
                Body = emailBody,
                IsBodyHTML = true,
                LinkedResources = { logoResource },         
            };

            if(emailType == EmailType.Create || emailType == EmailType.Update && attachmentData != null)
            {
                emailDto.Attachments = new List<AttachmentDto>
                {
                    new AttachmentDto
                    {
                        Type = ContentType.PDF,
                        FileName = GetPDFFileName(entity),
                        Data = attachmentData!
                    }
                };
            }

            await SendEmailAsync(emailDto);

            return Error.None;
        }

        private async Task<string> GetRenderedBody<TEntity>(TEntity entity, EmailType emailType)
        {
            var paramDictionary = GetDefaultHTMLParams(entity);

            return entity switch
            {
                HallRent hallRent => emailType switch
                {
                    EmailType.Cancel => await _htmlRenderer.RenderHtmlToStringAsync<HallRentCancelEmailBody>(paramDictionary),
                    EmailType.Create => await _htmlRenderer.RenderHtmlToStringAsync<HallRentCreateEmailBody>(paramDictionary),
                    _ => throw new ArgumentOutOfRangeException(nameof(emailType), $"Unsupported email body type: {emailType}")
                },
                EventPass eventPass => emailType switch
                {
                    EmailType.Cancel => await _htmlRenderer.RenderHtmlToStringAsync<EventPassCancelEmailBody>(paramDictionary),
                    EmailType.Create => await _htmlRenderer.RenderHtmlToStringAsync<EventPassEmailBody>(paramDictionary),
                    EmailType.Update => await _htmlRenderer.RenderHtmlToStringAsync<EventPassRenewEmailBody>(paramDictionary),
                    _ => throw new ArgumentOutOfRangeException(nameof(emailType), $"Unsupported email body type: {emailType}")
                },
                Reservation reservation => emailType switch
                {
                    EmailType.Cancel => await _htmlRenderer.RenderHtmlToStringAsync<ReservationCancelEmailBody>(paramDictionary),
                    EmailType.Create => reservation.EventPass == null ?
                                            await _htmlRenderer.RenderHtmlToStringAsync<TicketEmailBody>(paramDictionary) :
                                            await _htmlRenderer.RenderHtmlToStringAsync<TicketEventPassEmailBody>(paramDictionary),

                    _ => throw new ArgumentOutOfRangeException(nameof(emailType), $"Unsupported email body type: {emailType}")
                },
                _ => throw new ArgumentOutOfRangeException(nameof(entity), $"Unsupported type of entity: {entity!.GetType().Name}")
            };
        }

        private string GetMailSubject<TEntity>(TEntity entity, EmailType emailType)
        {
            return entity switch
            {
                EventPass eventPass => emailType switch
                {
                    EmailType.Update => string.Format(EventPassRenewMailSubject, eventPass.Id),
                    EmailType.Create => string.Format(EventPassCreateMailSubject, eventPass.Id),
                    EmailType.Cancel => string.Format(EventPassCancelMailSubject, eventPass.Id),
                    _ => throw new ArgumentOutOfRangeException(nameof(emailType), $"Unsupported email body type: {emailType}")
                },
                HallRent hallRent => emailType switch
                {
                    EmailType.Create => string.Format(HallRentCreateMailSubject, hallRent.Id),
                    EmailType.Cancel => string.Format(HallRentCancelMailSubject, hallRent.Id),
                    EmailType.Update => HallRentUpdateMailSubject,
                    _ => throw new ArgumentOutOfRangeException(nameof(emailType), $"Unsupported email body type: {emailType}")
                },
                Reservation reservation => emailType switch
                {
                    EmailType.Create => string.Format(ReservationCreateMailSubject, reservation.Id),
                    EmailType.Cancel => string.Format(ReservationCancelMailSubject, reservation.Id),
                    _ => throw new ArgumentOutOfRangeException(nameof(emailType), $"Unsupported email body type: {emailType}")
                },
                _ => throw new ArgumentOutOfRangeException(nameof(entity), $"Unsupported type of entity: {entity!.GetType().Name}")
            };
        }

        private string GetPDFFileName<TEntity>(TEntity entity)
        {
            return entity switch
            {
                EventPass eventPass => string.Format(EventPassPDFFileName, eventPass.Id),
                Reservation reservation => string.Format(ReservationPDFFileName, reservation.Id),
                HallRent hallRent => string.Format(HallRentPDFFileName, hallRent.Id),
                _ => throw new ArgumentOutOfRangeException(nameof(entity), $"Unsupported type of entity: {entity!.GetType().Name}")
            };
        }

        public async Task SendInfoAboutCanceledHallRents(List<HallRent> hallRentsToDelete)
        {
            var paramDictionary = GetCancelHallRentsHTMLParams(hallRentsToDelete);
            var htmlStringEmailBody = await _htmlRenderer.RenderHtmlToStringAsync<HallDeleteHallRentCancelEmailBody>(paramDictionary);

            var hallRent = hallRentsToDelete.First();

            var emailDto = new EmailDto
            {
                Email = hallRent.User.Email!,
                Subject = HallRentsCancelMailSubject,
                Body = htmlStringEmailBody,
                IsBodyHTML = true,
                LinkedResources = { logoResource },
            };
            await SendEmailAsync(emailDto);
        }


        public async Task SendInfoAboutCanceledEvents(List<(Reservation, bool)> deleteReservationsInfo, Event? eventEntity = null)
        {
            var reservationList = deleteReservationsInfo.Select(x => x.Item1);
            var reservation = reservationList.First();
            var paramDictionary = GetCancelEventHTMLParams(deleteReservationsInfo, eventEntity);
            var htmlStringEmailBody = await _htmlRenderer.RenderHtmlToStringAsync<EventCancelEmailBody>(paramDictionary);

            var emailDto = new EmailDto
            {
                Email = reservation.User.Email!,
                Subject = eventEntity == null ? CancelEventsMailSubject : CancelEventMailSubject,
                Body = htmlStringEmailBody,
                IsBodyHTML = true,
                LinkedResources = { logoResource },
            };
            await SendEmailAsync(emailDto);
        }


        public async Task SendUpdatedHallRentsAsync(List<(HallRent, byte[])> tupleList, Hall oldHall)
        {
            var hallRents = tupleList.Select(t => t.Item1).ToList();

            var paramDictionary = GetUpdatedHallRentHTMLParams(hallRents, oldHall);

            string htmlStringEmailBody = await _htmlRenderer.RenderHtmlToStringAsync<HallRentUpdateEmailBody>(paramDictionary);

            var hallRent = hallRents.First();

            var emailDto = new EmailDto
            {
                Email = hallRent.User.Email!,
                Subject = GetMailSubject(hallRent, EmailType.Update),
                Body = htmlStringEmailBody,
                IsBodyHTML = true,
                LinkedResources = { logoResource },
                Attachments = GetListOfUpdatedTicketsAttachments(tupleList)
            };
            await SendEmailAsync(emailDto);
        }

        public async Task SendUpdatedTicketsAsync(List<(Reservation, byte[])> tupleList, OldEventInfo oldEventInfo)
        {
            var reservationList = tupleList.Select(t => t.Item1);
            var reservation = reservationList.First();
            var newEventInfo = reservation.Ticket.Event;
            var reservationIds = string.Join(", ", reservationList.Select(r => r.Id));

            var paramDictionary = GetUpdatedTicketHTMLParams(reservationIds, oldEventInfo, newEventInfo);

            string htmlStringEmailBody = await _htmlRenderer.RenderHtmlToStringAsync<EventUpdateEmailBody>(paramDictionary);

            var eventName = oldEventInfo.Name == null ? newEventInfo.Name : oldEventInfo.Name;
            var emailDto = new EmailDto
            {
                Email = reservation.User.Email!,
                Subject = string.Format(UpdateTicketMailSubject, eventName),
                Body = htmlStringEmailBody,
                IsBodyHTML = true,
                LinkedResources = { logoResource },
                Attachments = GetListOfUpdatedTicketsAttachments(tupleList)
            };
            await SendEmailAsync(emailDto);
        }



        private List<AttachmentDto> GetListOfUpdatedTicketsAttachments<TEntity>(List<(TEntity, byte[])> tupleList)
        {
            List<AttachmentDto> attachmentList = [];
            foreach(var tuple in tupleList)
            {
                var entity = tuple.Item1;
                var ticketPDF = tuple.Item2;

                var attachmentDto = new AttachmentDto
                {
                    Type = ContentType.PDF,
                    FileName = GetPDFFileName(entity), 
                    Data = ticketPDF
                };
                attachmentList.Add(attachmentDto);
            }
            return attachmentList;
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

        private Dictionary<string, object?> GetUpdatedTicketHTMLParams(string reservationIds, OldEventInfo oldEventInfo, Event newEventInfo)
        {
            return new Dictionary<string, object?>()
            {
                { "ReservationIds", reservationIds },
                { "LogoContentId", LogoContentId },
                { "OldEventInfo", oldEventInfo },
                { "NewEventInfo", newEventInfo }
            };
        }

        private Dictionary<string, object?> GetCancelEventHTMLParams(List<(Reservation, bool)> deleteReservationsInfo, Event? eventEntity = null)
        {
            return new Dictionary<string, object?>()
            {
                { "DeleteReservationsInfo", deleteReservationsInfo },
                { "LogoContentId", LogoContentId },
                { "EventEntity", eventEntity }
            };
        }

        private Dictionary<string, object?> GetUpdatedHallRentHTMLParams(List<HallRent> hallRents, Hall oldHall)
        {
            return new Dictionary<string, object?>()
            {
                { "HallRents", hallRents },
                { "LogoContentId", LogoContentId },
                { "OldHall", oldHall }
            };
        }

        private Dictionary<string, object?> GetCancelHallRentsHTMLParams(List<HallRent> hallRents)
        {
            return new Dictionary<string, object?>()
            {
                { "HallRents", hallRents },
                { "LogoContentId", LogoContentId },
            };
        }


        private Dictionary<string, object?> GetDefaultHTMLParams<TEntity>(TEntity entity)
        {
            var entityTypeName = entity!.GetType().Name.ToString();
            return new Dictionary<string, object?>()
            {
                { entityTypeName, entity },
                { "LogoContentId", LogoContentId }
            };
        }

    }
}
