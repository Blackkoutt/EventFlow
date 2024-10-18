using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Helpers.PdfOptions.PdfCommonOptions
{
    public class LineOptions
    {
        public float PadTop { get; set; }
        public float PadLeft { get; set; }
        public float PadRight { get; set; }
        public float PadBottom { get; set; }
        public float Width { get; set; }
        public Unit Unit { get; set; }
    }
}
