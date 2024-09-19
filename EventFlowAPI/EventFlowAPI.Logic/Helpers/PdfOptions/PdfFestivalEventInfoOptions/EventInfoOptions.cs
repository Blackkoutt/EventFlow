using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfTextOptions;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Helpers.PdfOptions.PdfFestivalEventInfoOptions
{
    public class EventInfoOptions(Reservation reservation)
    {
        private readonly string defaultFontType = FontType.Inter.ToString();
        private readonly Reservation _reservation = reservation;


        public TextOptions StartDate => new TextOptions
        {
            Text = $"{_reservation.Ticket.Event.StartDate.ToString(DateFormat.DateTimeFullDayAndMonth).ToUpper()}",
            Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(13f),
            PaddingBottom = 2.5f
        };


        public TextOptions Hall => new TextOptions
        {
            Text = $"SALA {_reservation.Ticket.Event.Hall.HallNr}",
            Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(13f),
            PaddingBottom = 5f
        };


        public TextOptions Category => new TextOptions
        {
            Text = $"{_reservation.Ticket.Event.Category.Name.ToUpper()}",
            Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(14f),
            PaddingBottom = 0f
        };


        public TextOptions Name => new TextOptions
        {
            Text = $"{_reservation.Ticket.Event.Name.ToUpper()}",
            Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(14f).Bold(),
            PaddingBottom = 0f
        };
    }
}
