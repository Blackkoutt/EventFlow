using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Helpers.PdfOptions.PdfTextOptions
{
    public class SummaryTextOptions
    {
        public string Label { get; set; } = string.Empty;
        public float LabelWidth { get; set; }
        public string Value { get; set; } = string.Empty;
        public float ValueWidth { get; set; }
        public string TextBackgound { get; set; } = string.Empty;
        public float PadVertical { get; set; }
        public float PadHorizontal { get; set; }
        public TextStyle Style { get; set; } = default!;
    }
}
