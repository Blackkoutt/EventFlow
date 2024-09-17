using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Helpers.PdfOptions.PdfCommonOptions
{
    public class BottomLineOptions : AbstractLineOptions
    {
        public override float PadTop => 10f;

        public override float PadBottom => 10f;

        public override float Width => 1f;

        public override Unit Unit => Unit.Point;
    }
}
