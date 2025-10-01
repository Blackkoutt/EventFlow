using EventFlowAPI.DB.Entities;
using EventFlowAPI.DB.Entities.Abstract;
using EventFlowAPI.Logic.Enums;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfTextOptions;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Helpers.PdfOptions.PdfSummaryOptions
{
    public sealed class EventPassTypeOptions(List<EventPassType> eventPassTypes) : DescriptionOptions
    {
        public sealed override TextOptions Header => new TextOptions
        {
            Text = "Typy karnetów (cena / zniżka przy przedłużeniu):",
            Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(9.5f)
        };

        public sealed override List<IEntity> GetList => eventPassTypes.Select(st => (IEntity)st).ToList();

        public sealed override TextOptions GetListItemString(IEntity item)
        {
            var eventPassType = (EventPassType)item;
            return new TextOptions
            {
                Text = $"- {eventPassType.Name}: {eventPassType.Price} {Currency.PLN} / -{eventPassType.RenewalDiscountPercentage}%",
                Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(8.5f)
            };
        }
    }
}
