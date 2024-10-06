using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Helpers.PdfOptions.PdfTextOptions
{
    public class LabelValueTextOptions
    {
        public string Label { get; set; } = string.Empty;     
        public TextStyle LabelStyle { get; set; } = default!;
        public string Value { get; set; } = string.Empty;
        public TextStyle ValueStyle { get; set; } = default!;
        public float PaddingBottom { get; set; } = 0f;
        public float PaddingTop { get; set; } = 0f;
        public float Space { get; set; } = 5f;
    }
}
