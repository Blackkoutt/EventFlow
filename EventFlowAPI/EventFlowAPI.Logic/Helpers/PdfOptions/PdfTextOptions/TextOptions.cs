using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Helpers.PdfOptions.PdfTextOptions
{
    public class TextOptions
    {
        public string Text { get; set; } = string.Empty;
        public TextStyle Style { get; set; } = default!;
        public float PaddingBottom { get; set; } = 0f;
        public float PaddingTop { get; set; } = 0f;
    }
}
