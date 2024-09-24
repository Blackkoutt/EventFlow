using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfTextOptions;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Helpers.PdfOptions.PdfSummaryOptions
{
    public class EventPassSummaryOptions : SummaryOptions
    {
        private readonly EventPass _eventPass;

        public EventPassSummaryOptions(EventPass eventPass, List<EventPassType> eventPassTypes)
        {
            _eventPass = eventPass;
            EventPassType = new(eventPassTypes);
        }

        public EventPassTypeOptions EventPassType { get; private set; }

        protected override string PriceLabel => "Cena karnetu";
        protected override string PriceValue => $"{_eventPass.PassType.Price} {Currency.PLN}";
        protected override string TotalCostValue => $"{_eventPass.PaymentAmount} {Currency.PLN}";

        public SummaryTextOptions Discount => new SummaryTextOptions
        {
            Label = $"Zniżka ({_eventPass.TotalDiscountPercentage}):",
            Value = $"{_eventPass.TotalDiscount} {Currency.PLN}",
            TextBackgound = "#ededed",
            Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(11f),
            PadHorizontal = 8,
            PadVertical = 4,
            LabelWidth = 2,
            ValueWidth = 1
        };
    }
}
