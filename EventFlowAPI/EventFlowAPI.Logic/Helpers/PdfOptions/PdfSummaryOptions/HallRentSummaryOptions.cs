using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfTextOptions;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Helpers.PdfOptions.PdfSummaryOptions
{
    public class HallRentSummaryOptions : SummaryOptions
    {
        private readonly HallRent _hallRent;
        private readonly List<AdditionalServices> _additionalServices;
        public HallRentSummaryOptions(HallRent hallRent, List<AdditionalServices> additionalServices)
        {
            _hallRent = hallRent;
            AdditionalServices = new(additionalServices);
            _additionalServices = additionalServices;
        }
        public AdditionalServicesOptions AdditionalServices { get; private set; }
        public List<AdditionalServices> AdditionalServicesList => _additionalServices;
        public HallRent HallRent => _hallRent;
        protected override string PriceLabel => "Cena wynajmu (za 1h):";
        protected override string PriceValue => $"{_hallRent.Hall.RentalPricePerHour} {Currency.PLN}";
        protected override string TotalCostValue => $"{_hallRent.PaymentAmount} {Currency.PLN}";

        public SummaryTextOptions Hours => new SummaryTextOptions
        {
            Label = $"Ilość godzin:",
            Value = $"{Math.Ceiling((_hallRent.EndDate - _hallRent.StartDate).TotalHours)} h",
            TextBackgound = "#ededed",
            Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(11f),
            PadHorizontal = 8,
            PadVertical = 4,
            LabelWidth = 2,
            ValueWidth = 1
        };

        public SummaryTextOptions SubTotal => new SummaryTextOptions
        {
            Label = $"Podsuma:",
            Value = $"{Math.Round(_hallRent.Hall.RentalPricePerHour * (decimal)Math.Ceiling((_hallRent.EndDate - _hallRent.StartDate).TotalHours), 2)} {Currency.PLN}",
            TextBackgound = "#ededed",
            Style = TextStyle.Default.FontFamily(defaultFontType).SemiBold().FontSize(11f),
            PadHorizontal = 8,
            PadVertical = 4,
            LabelWidth = 2,
            ValueWidth = 1
        };

        public SummaryTextOptions AdditionalServicesHeader => new SummaryTextOptions
        {
            Label = $"Dodatkowe usługi:",
            Value = "",
            TextBackgound = "#ededed",
            Style = TextStyle.Default.FontFamily(defaultFontType).SemiBold().FontSize(11f),
            PadHorizontal = 8,
            PadVertical = 4,
            LabelWidth = 2,
            ValueWidth = 1
        };

        public SummaryTextOptions GetAdditionalServiceCost(AdditionalServices additionalService) => 
            new SummaryTextOptions
            {
                Label = $"{additionalService.Name}: ",
                Value = $"+ {Math.Round(additionalService.Price), 2} {Currency.PLN}",
                TextBackgound = "#ededed",
                Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(11f),
                PadHorizontal = 8,
                PadVertical = 4,
                LabelWidth = 2,
                ValueWidth = 1
            };
    }
}
