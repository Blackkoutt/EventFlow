using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfTextOptions;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Helpers.PdfOptions.PdfContentOptions
{
    public class ContentFestivalOptions(Reservation reservation)
    {
        private readonly string defaultFontType = FontType.Inter.ToString();
        private readonly Reservation _reservation = reservation;


        public TextOptions StartDate => new TextOptions
        {
            Text = $"{_reservation.Ticket.Festival!.StartDate.ToString(DateFormat.DateTimeFullDayAndMonth).ToUpper()}",
            Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(13f),
            PaddingBottom = 5f
        };


        public TextOptions Category => new TextOptions
        {
            Text = "FESTIWAL",
            Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(14f),
            PaddingBottom = 0f
        };


        public TextOptions Name => new TextOptions
        {
            Text = $"{_reservation.Ticket.Festival!.Name.ToUpper()}",
            Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(16f).Bold(),
            PaddingBottom = 0f
        };
    }
}
