using EventFlowAPI.DB.Entities;
using EventFlowAPI.DB.Entities.Abstract;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfTextOptions;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Helpers.PdfOptions.PdfSummaryOptions
{
    public sealed class AdditionalPaymentOptions(List<SeatType> seatTypes) : DescriptionOptions
    {
        public sealed override TextOptions Header => new TextOptions
        {
            Text = "Dodatkowe opłaty:",
            Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(9.5f)
        };

        public sealed override List<IEntity> GetList => seatTypes.Select(st => (IEntity)st).ToList();

        public sealed override TextOptions GetListItemString(IEntity item)
        {
            var seatType = (SeatType)item;  
            return new TextOptions
            {
                Text = $"- {seatType.Name} +{seatType.AddtionalPaymentPercentage}%",
                Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(8.5f)
            };
        }
    }
}
