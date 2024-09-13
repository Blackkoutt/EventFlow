using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Services.OtherServices.Extensions;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces.TicketConfiguration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;

namespace EventFlowAPI.Logic.Services.OtherServices.Services
{
    public class TicketCreatorService(
        IFestivalTicketConfiguration festivalTicketConfig,
        IEventTicketConfiguration eventTicketConfig,
        IQRCodeGeneratorService qrCoder) : ITicketCreatorService
    {
        private readonly IFestivalTicketConfiguration _festivalTicketConfig = festivalTicketConfig;
        private readonly IEventTicketConfiguration _eventTicketConfig = eventTicketConfig;
        private readonly IQRCodeGeneratorService _qrCoder = qrCoder;

        public async Task<int> CreateEventTicketPNG(Reservation reservation)
        {
            var outputPath = _eventTicketConfig.GetAssetPath(AssetType.Tests, "EventPath");

            using (var image = await _eventTicketConfig.GetTicketTemplate(Template.EventTicket))
            {
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
                var qrCode = _qrCoder.GenerateQRCode(reservation, qrCodeOptions.Size);
                image.Draw(qrCode, qrCodeOptions);
                
                await image.SaveAsPngAsync(outputPath);
                return 1;

                /*using (var memoryStream = new MemoryStream())
                {
                   
                    //return memoryStream.ToArray();
                    
                }*/
            }
        }

        public async Task<int> CreateFestivalTicketPNG(Festival festival, List<Reservation> reservations)
        {
            var outputPath = _festivalTicketConfig.GetAssetPath(AssetType.Tests, "FestivalPathFront");

            using (var template = await _festivalTicketConfig.GetTicketTemplate(Template.FestivalTicketFront))
            {
                var ticket = CreateFestivalFrontOfTicket(template, festival, reservations.First());

                await ticket.SaveAsPngAsync(outputPath);
            }

            List<Image> reverseOfTickets = [];
            var outputReverseTicketPath = _festivalTicketConfig.GetAssetPath(AssetType.Tests, "FestivalPathReverse");
            using (var template = await _festivalTicketConfig.GetTicketTemplate(Template.FestivalTicketReverse))
            {
                reverseOfTickets = CreateFestivalReverseOfTickets(template, reservations);
                for (int i = 0; i < reverseOfTickets.Count; i++)
                {
                    var outputName = $"festival_reverse_test{i + 1}.png";
                    var reverseOutputPath = $"{outputReverseTicketPath}{outputName}";
                    await reverseOfTickets[i].SaveAsPngAsync(reverseOutputPath);
                }
            }

            
            

            return 1;
        }

        private Image CreateFestivalFrontOfTicket(Image template, Festival festival, Reservation reservation)
        {
            var titleOptions = _festivalTicketConfig.GetTitlePrintingOptions(festival);
            template.Draw(festival.Name, titleOptions);

            var dateOptions = _festivalTicketConfig.GetDatePrintingOptions();
            template.Draw($"{festival.StartDate.ToString(dateOptions.DateFormat)} - " +
                $"{festival.EndDate.ToString(dateOptions.DateFormat)}", dateOptions);

            var qrCodeOptions = _festivalTicketConfig.GetQRCodePrintingOptions();
            var qrCode = _qrCoder.GenerateQRCode(reservation, qrCodeOptions.Size);
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
