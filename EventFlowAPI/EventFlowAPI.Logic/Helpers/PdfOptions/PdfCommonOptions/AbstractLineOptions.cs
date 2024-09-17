using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Helpers.PdfOptions.PdfCommonOptions
{
    public abstract class AbstractLineOptions
    {
        public abstract float PadTop { get; }
        public abstract float PadBottom { get; }
        public abstract float Width { get; }
        public abstract Unit Unit { get; }
    }
}
