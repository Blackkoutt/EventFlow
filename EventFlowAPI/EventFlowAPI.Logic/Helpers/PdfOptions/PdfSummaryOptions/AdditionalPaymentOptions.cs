using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfTextOptions;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Helpers.PdfOptions.PdfSummaryOptions
{
    public class AdditionalPaymentOptions(List<SeatType> seatTypes)
    {
        private readonly string defaultFontType = FontType.Inter.ToString();
        public float ColumnSpacing => 1f;
        public List<SeatType> SeatTypes => seatTypes;

        public TextOptions Header => new TextOptions
        {
            Text = "Dodatkowe opłaty:",
            Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(10f)
        };


        public TextOptions GetSeatTypeString(SeatType seatType)
        {
            return new TextOptions
            {
                Text = $"- {seatType.Name} +{seatType.AddtionalPaymentPercentage}%",
                Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(9f)
            };
        }
    }
}
