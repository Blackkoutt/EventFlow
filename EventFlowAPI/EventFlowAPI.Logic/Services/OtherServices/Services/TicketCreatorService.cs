using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Services.OtherServices.Extensions;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces;
using Microsoft.Extensions.Configuration;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;

namespace EventFlowAPI.Logic.Services.OtherServices.Services
{
    public class TicketCreatorService(IConfiguration configuration) : ITicketCreatorService
    {
        private readonly IConfiguration _configuration = configuration; 

        public async Task<int> CreateEventTicketJPG(Reservation reservation)
        {
            var ticketTemplatePath = Path.GetFullPath(_configuration.GetSection("Assets:Templates")["Ticket"]!);
            var fontPath = Path.GetFullPath(_configuration.GetSection("Assets:Fonts")["Inter-Regular"]!);
            var outputPath = Path.GetFullPath(_configuration.GetSection("Assets:OutputTest")["Path"]!);

            using (var image = Image.Load(ticketTemplatePath))
            {
                var brushColor = Color.Black;
                
                var fontCollection = new FontCollection();
                var fontFamily = fontCollection.Add(fontPath);

                var fontTitle = fontFamily.CreateFont(48, FontStyle.Bold);

                //236 w
                //43

                int margin = 43;
                int rightMax = 791;
                int maxSpaceForTextInOneLine = rightMax - 2 * margin;
                int estimatedCharWidth = 24;
                int estimatedMaxCharHeight = 35;
                int maxCharCountInOneLine = maxSpaceForTextInOneLine / estimatedCharWidth;
                

                var eventEntity = reservation.Ticket.Event;
                var eventTitle = $"{eventEntity.Category.Name}: {eventEntity.Name}";
                var charCount = eventTitle.Length;

                var firstLineY = 230;
                var secondLineY = firstLineY + estimatedMaxCharHeight + 20;
                string firstLine = eventTitle;
                string secondLine = string.Empty;

                int firstLineWidth = firstLine.Length * estimatedCharWidth;
                int firstLineStartPositionToBeCentred = margin + ((maxSpaceForTextInOneLine - firstLineWidth) / 2);

                if (charCount > maxCharCountInOneLine)
                {
                    var lastSpaceIndex = eventTitle.Substring(0, 26).LastIndexOf(' ');
                    firstLine = eventTitle.Substring(0, lastSpaceIndex);
                    secondLine = eventTitle.Substring(lastSpaceIndex).TrimStart();

                    firstLineWidth = firstLine.Length * estimatedCharWidth;
                    firstLineStartPositionToBeCentred = margin + ((maxSpaceForTextInOneLine - firstLineWidth) / 2);

                    int secondLineWidth = secondLine.Length * estimatedCharWidth;
                    int secondLineStartPositionToBeCentred = margin + ((maxSpaceForTextInOneLine - secondLineWidth)/2);

                    image.Draw(
                        text: secondLine,
                        font: fontTitle,
                        textColor: brushColor,
                        location: new PointF(secondLineStartPositionToBeCentred, secondLineY)
                    );
                }

                image.Draw(
                    text: firstLine,
                    font: fontTitle,
                    textColor: brushColor,
                    location: new PointF(firstLineStartPositionToBeCentred, firstLineY)
                );

                var fontDate = fontFamily.CreateFont(40, FontStyle.Bold);

                image.Draw(
                    text: $"{eventEntity.StartDate.ToString("dd.MM.yyyy HH:mm")}",
                    font: fontDate,
                    textColor: brushColor,
                    location: new PointF(230, 426)
                );

                var fontDetails = fontFamily.CreateFont(25, FontStyle.Bold);


                int estimatedCharWidthDetails = 12;
                int rightMaxPrice = 256;
                int leftMaxPrice = 73;
                int spaceForPrice = rightMaxPrice - leftMaxPrice;
                int priceStringLength = reservation.Ticket.Price.ToString().Length + 3;
                int priceMargin = leftMaxPrice + (spaceForPrice - (priceStringLength * estimatedCharWidthDetails)) / 2; 

                image.Draw(
                    text: $"{reservation.Ticket.Price} zł",
                    font: fontDetails,
                    textColor: brushColor,
                    location: new PointF(priceMargin, 546)
                );

                image.Draw(
                    text: eventEntity.Hall.HallNr.ToString(),
                    font: fontDetails,
                    textColor: brushColor,
                    location: new PointF(371, 546)
                );

                int rightMaxDuration = 717;
                int leftMaxDuration = 507;
                int spaceForDuration = rightMaxDuration - leftMaxDuration;
                int durationStringLength = eventEntity.Duration.TotalMinutes.ToString().Length + 4;
                int durationMargin = leftMaxDuration + (spaceForDuration - (durationStringLength * estimatedCharWidthDetails)) / 2;
                
                image.Draw(
                    text: $"{eventEntity.Duration.TotalMinutes} min",
                    font: fontDetails,
                    textColor: brushColor,
                    location: new PointF(durationMargin, 546)
                );

                var fontSeats = fontFamily.CreateFont(32, FontStyle.Bold);

                var seatsNr = reservation.Seats.Select(s => s.SeatNr);


                image.Draw(
                    text: string.Join(", ", seatsNr),
                    font: fontSeats,
                    textColor: brushColor,
                    location: new PointF(865, 85)
                );


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
