using EventFlowAPI.DB.Entities;
using EventFlowAPI.DB.Entities.Abstract;
using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfTextOptions;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Helpers.PdfOptions.PdfSummaryOptions
{
    public sealed class TicketTypeOptions(List<Ticket> tickets) : DescriptionOptions
    {

        public sealed override TextOptions Header => new TextOptions
        {
            Text = "Typy biletów:",
            Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(9.5f)
        };

        public sealed override List<IEntity> GetList => tickets.Select(st => (IEntity)st).ToList();

        public sealed override TextOptions GetListItemString(IEntity item)
        {
            var ticket = (Ticket)item;
            return new TextOptions
            {
                Text = $"- {ticket.TicketType.Name} {Math.Round(ticket.Price, 2)} {Currency.PLN}",
                Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(8.5f)
            };
        }
    }
}
