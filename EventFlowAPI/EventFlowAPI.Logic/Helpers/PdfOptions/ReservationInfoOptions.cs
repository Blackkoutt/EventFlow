using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfTextOptions;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Helpers.PdfOptions
{
    public class ReservationInfoOptions(Reservation reservation)
    {
        private readonly Reservation _reservation = reservation;
        private readonly string defaultFontType = FontType.Inter.ToString();
        public float PadLeft => 10f;

        public TextOptions OrderNumber => new TextOptions
        {
            Text = $"Numer zamówienia {_reservation.Id}",
            PaddingBottom = 4f,
            Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(12f).SemiBold()
        };


        public TextOptions TicketOrderDate => new TextOptions
        {
            Text = $"Bilet zamówiony {_reservation.ReservationDate.ToString(DateFormat.DateTimeFullMonth)}",
            PaddingBottom = 2.5f,
            Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(11f)
        };


        public TextOptions TicketType => new TextOptions
        {
            Text = $"Typ biletu: {_reservation.Ticket.TicketType.Name}",
            PaddingBottom = 2.5f,
            Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(11f)
        };


        public TextOptions PaymentType => new TextOptions
        {
            Text = $"Typ płatności: {_reservation.PaymentType.Name}",
            PaddingBottom = 2.5f,
            Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(11f)
        };


        public TextOptions OrderingPerson => new TextOptions
        {
            Text = $"Osoba zamawiająca: {_reservation.User.Name} {_reservation.User.Surname}",
            PaddingBottom = 0f,
            Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(11f)
        };       
    }
}
