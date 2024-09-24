using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfTextOptions;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Helpers.PdfOptions.PdfSummaryOptions
{
    public class EventPassTypeOptions(List<EventPassType> eventPassTypes)
    {
        private readonly string defaultFontType = FontType.Inter.ToString();
        public float ColumnSpacing => 1f;
        public List<EventPassType> EventPassTypes => eventPassTypes;

        public TextOptions Header => new TextOptions
        {
            Text = "Typy karnetów (cena / zniżka przy przedłużeniu):",
            Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(9.5f)
        };

        public TextOptions GetEventPassTypeString(EventPassType eventPassType)
        {
            return new TextOptions
            {
                Text = $"- {eventPassType.Name}: {eventPassType.Price} {Currency.PLN} / {eventPassType.RenewalDiscountPercentage}%",
                Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(8.5f)
            };
        }
    }
}
