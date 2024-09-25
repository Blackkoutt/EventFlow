using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfTextOptions;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Helpers.PdfOptions.PdfSummaryOptions
{
    public class TicketTypeOptions(List<Ticket> tickets)
    {
        private readonly string defaultFontType = FontType.Inter.ToString();
        public float ColumnSpacing => 1f;
        public List<Ticket> Tickets => tickets;


        public TextOptions Header => new TextOptions
        {
            Text = "Typy biletów:",
            Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(9.5f)
        };


        public TextOptions GetTicketTypeString(Ticket ticket)
        {
            return new TextOptions
            {
                Text = $"- {ticket.TicketType.Name} {Math.Round(ticket.Price,2)} {Currency.PLN}",
                Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(8.5f)
            };
        }
    }
}
