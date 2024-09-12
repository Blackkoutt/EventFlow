using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Helpers;
using EventFlowAPI.Logic.Helpers.TicketOptions;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces.TicketCreatorConfiguration;
using EventFlowAPI.Logic.Services.OtherServices.Services.TicketCreatorConfiguration.Abstract;
using Microsoft.Extensions.Configuration;
using SixLabors.Fonts;
using SixLabors.ImageSharp;

namespace EventFlowAPI.Logic.Services.OtherServices.Services.TicketCreatorConfiguration
{
    public class EventTicketCreatorConfigurationService(IConfiguration configuration) :
        TicketCreatorConfigurationService(configuration),
        IEventTicketCreatorConfigurationService
    {
        public sealed override async Task<Image> GetTicketTemplate()
        {
            var ticketPath = GetAssetPath(AssetType.Templates, "EventTicket");
            return await Image.LoadAsync(ticketPath);
        }

        public TicketTitlePrintingOptions GetTitlePrintingOptions(Event eventEntity)
        {
            const int titleFirstLineY = 230;
            const int titleLineHeight = 20;
            const int margin = 43;
            const int rightMax = 791;
            const int estimatedCharWidth = 24;
            const int estimatedMaxCharHeight = 35;

            int maxSpaceForText = rightMax - 2 * margin;
            int maxCharCountInOneLine = maxSpaceForText / estimatedCharWidth;

            var eventTitle = $"{eventEntity.Category.Name}: {eventEntity.Name}";
            var charCount = eventTitle.Length;

            var firstLineY = titleFirstLineY;
            var secondLineY = firstLineY + estimatedMaxCharHeight + titleLineHeight;
            int secondLineStartPositionToBeCentred = 0;

            string firstLine = eventTitle;
            string secondLine = string.Empty;

            int firstLineWidth = firstLine.Length * estimatedCharWidth;
            int firstLineStartPositionToBeCentred = margin + (maxSpaceForText - firstLineWidth) / 2;

            if (charCount > maxCharCountInOneLine)
            {
                var lastSpaceIndex = eventTitle.Substring(0, maxCharCountInOneLine).LastIndexOf(' ');
                firstLine = eventTitle.Substring(0, lastSpaceIndex);
                secondLine = eventTitle.Substring(lastSpaceIndex).TrimStart();

                firstLineWidth = firstLine.Length * estimatedCharWidth;
                firstLineStartPositionToBeCentred = margin + (maxSpaceForText - firstLineWidth) / 2;

                int secondLineWidth = secondLine.Length * estimatedCharWidth;
                secondLineStartPositionToBeCentred = margin + (maxSpaceForText - secondLineWidth) / 2;
            }

            return new TicketTitlePrintingOptions
            {
                TitleFirstLine = firstLine,
                TitleSecondLine = secondLine,
                FirstLineLocation = new PointF(firstLineStartPositionToBeCentred, firstLineY),
                SecondLineLocation = new PointF(secondLineStartPositionToBeCentred, secondLineY),
                Font = GetFont(48, FontStyle.Bold),
                BrushColor = Color.Black
            };
        }

        public TicketDatePrintingOptions GetDatePrintingOptions()
        {
            return new TicketDatePrintingOptions
            {
                Font = GetFont(40, FontStyle.Bold),
                BrushColor = Color.Black,
                Location = new PointF(230, 426),
                DateFormat = "dd.MM.yyyy HH:mm"
            };
        }
        public TicketPricePrintingOptions GetPricePrintingOptions(Reservation reservation)
        {
            const int leftMax = 73;
            const int rightMax = 256;
            const int estimatedCharWidth = 12;

            int maxSpaceForText = rightMax - leftMax;
            int priceStringLength = reservation.Ticket.Price.ToString().Length + 3;
            int margin = leftMax + (maxSpaceForText - priceStringLength * estimatedCharWidth) / 2;

            return new TicketPricePrintingOptions
            {
                Font = GetFont(25, FontStyle.Bold),
                BrushColor = Color.Black,
                Location = new PointF(margin, 546),
                Currency = "zł"
            };
        }

        public TicketPrintingOptions GetHallPrintingOptions()
        {
            return new TicketPrintingOptions
            {
                Font = GetFont(25, FontStyle.Bold),
                BrushColor = Color.Black,
                Location = new PointF(371, 546)
            };
        }

        public TicketPrintingOptions GetDurationPrintingOpitons(Event eventEntity)
        {
            const int rightMax = 717;
            const int leftMax = 507;
            const int estimatedCharWidth = 12;

            int spaceForDuration = rightMax - leftMax;
            int durationStringLength = eventEntity.Duration.TotalMinutes.ToString().Length + 4;
            int margin = leftMax + (spaceForDuration - durationStringLength * estimatedCharWidth) / 2;

            return new TicketPrintingOptions
            {
                Font = GetFont(25, FontStyle.Bold),
                BrushColor = Color.Black,
                Location = new PointF(margin, 546)
            };
        }

        public TicketPrintingOptions GetSeatsPrintingOptions()
        {
            return new TicketPrintingOptions
            {
                Font = GetFont(32, FontStyle.Bold),
                BrushColor = Color.Black,
                Location = new PointF(865, 85)
            };
        }

        public TicketQRCodePrintingOptions GetQRCodePrintingOptions()
        {
            return new TicketQRCodePrintingOptions
            {
                Location = new Point(1607, 132),
                Opacity = 1f
            };
        }
    }
}
