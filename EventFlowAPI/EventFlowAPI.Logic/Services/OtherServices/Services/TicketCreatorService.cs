using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Helpers;
using EventFlowAPI.Logic.Services.OtherServices.Extensions;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces.TicketCreatorConfiguration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;

namespace EventFlowAPI.Logic.Services.OtherServices.Services
{
    public class TicketCreatorService(IEventTicketCreatorConfigurationService eventTicketConfig, IQRCodeGeneratorService qrCoder) : ITicketCreatorService
    {
        private readonly IEventTicketCreatorConfigurationService _eventTicketConfig = eventTicketConfig;
        private readonly IQRCodeGeneratorService _qrCoder = qrCoder;        

        public async Task<int> CreateEventTicketJPG(Reservation reservation)
        {
            var outputPath = _eventTicketConfig.GetAssetPath(AssetType.Tests, "Path");

            using (var image = await _eventTicketConfig.GetTicketTemplate())
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

                var qrCode = _qrCoder.GenerateQRCode(reservation);
                var qrCodeOptions = _eventTicketConfig.GetQRCodePrintingOptions();
                image.Draw(qrCode, qrCodeOptions);
                
                await image.SaveAsPngAsync(outputPath);
                return 1;

                /*using (var memoryStream = new MemoryStream())
                {
                   
                    //return memoryStream.ToArray();
                    
                }*/
            }
        }       
    }
}
