using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfTextOptions;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Helpers.PdfOptions.PdfSummaryOptions
{
    public class TicketTypeOptions(List<TicketType> ticketTypes)
    {
        private readonly string defaultFontType = FontType.Inter.ToString();
        public float ColumnSpacing => 1f;
        public List<TicketType> TicketTypes => ticketTypes;


        public TextOptions Header => new TextOptions
        {
            Text = "Typy biletów:",
            Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(10f)
        };


        public TextOptions GetTicketTypeString(TicketType ticketType)
        {
            return new TextOptions
            {
                Text = $"- {ticketType.Name}",
                Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(9f)
            };
        }
    }
}
