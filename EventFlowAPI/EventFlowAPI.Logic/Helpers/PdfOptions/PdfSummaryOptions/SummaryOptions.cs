using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Enums;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfTextOptions;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Helpers.PdfOptions.PdfSummaryOptions
{
    public abstract class SummaryOptions
    {
        protected readonly string defaultFontType = FontType.Inter.ToString();
        
        
        // Summary Container
        public float ContainerPadLeft => 10f;
        public float ContainerRowSpacing => 5f;
        public float ContainerSummaryDescriptionRowWidth => 1f;
        public float ContainerSummaryRowWidth => 1f;

        // Summary
        public float PadRight => 10f;
        public float ColumnSpacing => 5f;
        public float ItemPadVertical => 4f;
        public float ItemPadHorizontal => 8f;


        protected abstract string PriceLabel { get; }
        protected abstract string PriceValue { get; }
        protected abstract string TotalCostValue { get; }

        public SummaryTextOptions Price => new SummaryTextOptions
        {
            Label = PriceLabel,
            Value = PriceValue,
            TextBackgound = "#ededed",
            Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(11f),
            PadHorizontal = 8,
            PadVertical = 4,
            LabelWidth = 2,
            ValueWidth = 1
        };
        public SummaryTextOptions TotalCost => new SummaryTextOptions
        {
            Label = "SUMA",
            Value = TotalCostValue,
            TextBackgound = "#ffffff",
            Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(12f).SemiBold(),
            PadHorizontal = 8,
            PadVertical = 4,
            LabelWidth = 2,
            ValueWidth = 1
        };
    }
}
