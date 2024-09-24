using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Extensions;
using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces.Configuration.PassConfiguration;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces.Configuration.TicketConfiguration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using System.IO;

namespace EventFlowAPI.Logic.Services.OtherServices.Services
{
    public class JPGCreatorService(
        IFestivalTicketConfiguration festivalTicketConfig,
        IEventTicketConfiguration eventTicketConfig,
        IEventPassConfiguration eventPassConfig,
        IQRCodeGeneratorService qrCoder,
        IAssetService assetService) : IJPGCreatorService
    {
        private readonly IFestivalTicketConfiguration _festivalTicketConfig = festivalTicketConfig;
        private readonly IEventTicketConfiguration _eventTicketConfig = eventTicketConfig;
        private readonly IEventPassConfiguration _eventPassConfig = eventPassConfig;
        private readonly IQRCodeGeneratorService _qrCoder = qrCoder;
        private readonly IAssetService _assetService = assetService;   

        public async Task<byte[]> CreateEventPass(EventPass eventPass)
        {
            // TEST
            var outputPath = _assetService.GetOutputTestPath(TestsOutput.EventPass);

            var image = await _assetService.GetTemplate(Template.EventPass);

            var passTypeOptions = _eventPassConfig.EventPassTypePrintingOptions;
            image.Draw(eventPass.PassType.Name, passTypeOptions);

            var passOwnerOptions = _eventPassConfig.EventPassOwnerPrintingOptions;
            image.Draw($"{eventPass.User.Name} {eventPass.User.Surname}", passOwnerOptions);

            var passDateOptions = _eventPassConfig.EventPassDatePrintingOptions;
            image.Draw($"{eventPass.EndDate.ToString(passDateOptions.DateFormat)}", passDateOptions);

            var qrCodeOptions =_eventPassConfig.EventPassQrCodePrintingOptions;
            var qrCode = _qrCoder.GenerateQRCode(eventPass.EventPassGuid.ToString(), qrCodeOptions.Size);
            image.Draw(qrCode, qrCodeOptions);

            await image.SaveAsJpegAsync(outputPath);

            return await image.AsBitmap(ImageFormat.JPEG);
        }

        public async Task<byte[]> CreateEventTicket(Reservation reservation)
        {
            // TEST
            var outputPath = _assetService.GetOutputTestPath(TestsOutput.EventPath);

            var image = await _assetService.GetTemplate(Template.EventTicket);
            var eventEntity = reservation.Ticket.Event;

            var titleOptions = _eventTicketConfig.GetTitlePrintingOptions(eventEntity);     
            image.Draw(eventEntity.Name, titleOptions);

            var dateOptions = _eventTicketConfig.GetDatePrintingOptions();
            image.Draw($"{eventEntity.StartDate.ToString(dateOptions.DateFormat)}", dateOptions);

            var priceOptions = _eventTicketConfig.GetPricePrintingOptions(reservation);
            image.Draw($"{reservation.Ticket.Price} {priceOptions.Currency}", priceOptions);

            var hallOptions = _eventTicketConfig.GetHallPrintingOptions();
            image.Draw(eventEntity.Hall.HallNr.ToString(), hallOptions);

            var durationOptions = _eventTicketConfig.GetDurationPrintingOpitons(eventEntity);
            image.Draw($"{eventEntity.Duration.TotalMinutes} min", durationOptions);
 
            var seatsOptions = _eventTicketConfig.GetSeatsPrintingOptions();
            image.Draw(string.Join(", ", reservation.Seats.Select(s => s.SeatNr)), seatsOptions);

            var qrCodeOptions = _eventTicketConfig.GetQRCodePrintingOptions();
            var qrCode = _qrCoder.GenerateQRCode(reservation.ReservationGuid.ToString(), qrCodeOptions.Size);
            image.Draw(qrCode, qrCodeOptions);
                
            // TEST
            await image.SaveAsJpegAsync(outputPath);

            return await image.AsBitmap(ImageFormat.JPEG);         
        }

        public async Task<List<byte[]>> CreateFestivalTicket(Festival festival, List<Reservation> reservations)
        {
            List<byte[]> imagesBitmaps = [];

            // TEST
            var outputPath = _assetService.GetOutputTestPath(TestsOutput.FestivalPathFront);

            var frontTemplate = await _assetService.GetTemplate(Template.FestivalTicketFront);
            var frontTicket = CreateFestivalFrontOfTicket(frontTemplate, festival, reservations.First());

            // TEST
            await frontTicket.SaveAsPngAsync(outputPath);

            var frontTicketBitmap = await frontTicket.AsBitmap(ImageFormat.JPEG);
            imagesBitmaps.Add(frontTicketBitmap);

            List<Image> reverseOfTickets = [];

            // TEST
            var outputReverseTicketPath = _assetService.GetOutputTestPath(TestsOutput.FestivalPathReverse);

            var reverseTemplate = await _assetService.GetTemplate(Template.FestivalTicketReverse);
            reverseOfTickets = CreateFestivalReverseOfTickets(reverseTemplate, reservations);

            for (int i = 0; i < reverseOfTickets.Count; i++)
            {
                // TEST
                var outputName = $"festival_reverse_test{i + 1}.png";
                var reverseOutputPath = $"{outputReverseTicketPath}{outputName}";
                await reverseOfTickets[i].SaveAsPngAsync(reverseOutputPath);

                var ticketReverse = await reverseOfTickets[i].AsBitmap(ImageFormat.JPEG);
                imagesBitmaps.Add(ticketReverse);
            }
            return imagesBitmaps;
        }

        private Image CreateFestivalFrontOfTicket(Image template, Festival festival, Reservation reservation)
        {
            var titleOptions = _festivalTicketConfig.GetTitlePrintingOptions(festival);
            template.Draw(festival.Name, titleOptions);

            var dateOptions = _festivalTicketConfig.GetDatePrintingOptions();
            template.Draw($"{festival.StartDate.ToString(dateOptions.DateFormat)} - " +
                $"{festival.EndDate.ToString(dateOptions.DateFormat)}", dateOptions);

            var qrCodeOptions = _festivalTicketConfig.GetQRCodePrintingOptions();
            var qrCode = _qrCoder.GenerateQRCode(reservation.ReservationGuid.ToString(), qrCodeOptions.Size);
            template.Draw(qrCode, qrCodeOptions);

            return template;
        }

        private List<Image> CreateFestivalReverseOfTickets(Image template, List<Reservation> reservations)
        {
            List<Image> reverseOfTickets = [];
            var defaultTemplate = template;

            int howManyReverseOfTicket = _festivalTicketConfig.GetReverseTicketCount(reservations.Count);
            int tabsCount = _festivalTicketConfig.TabsCount;

            for (int reverseNr = 0; reverseNr < howManyReverseOfTicket; reverseNr++)
            {
                template = defaultTemplate;

                int startResIndex = reverseNr * tabsCount;

                for (int rIndex = startResIndex; rIndex < reservations.Count; rIndex++)
                {
                    var tabNr = rIndex + 1;

                    Event eventEntity = reservations[rIndex].Ticket.Event;
                    var eventInfo = _festivalTicketConfig.GetEventInfo(eventEntity);
                    var seatsString = _festivalTicketConfig.GetSeatsString(reservations[rIndex]);

                    var tabNrOptions = _festivalTicketConfig.GetTabNumberPrintingOptions();
                    template.Draw($"{tabNr}.", tabNrOptions);

                    var eventInfoOptions = _festivalTicketConfig.GetEventInfoPrintingOpitons();
                    template.Draw(eventInfo, eventInfoOptions);

                    var seatsOptions = _festivalTicketConfig.GetSeatsPrintingOpitons();
                    template.Draw(seatsString, seatsOptions);

                    _festivalTicketConfig.MoveCursorToNextTab(tabNr);

                    if (_festivalTicketConfig.IsNextPageOfReverseTicket())
                    {
                        break;
                    }

                }
                reverseOfTickets.Add(template);
            }
            return reverseOfTickets;
        }
    }
}
