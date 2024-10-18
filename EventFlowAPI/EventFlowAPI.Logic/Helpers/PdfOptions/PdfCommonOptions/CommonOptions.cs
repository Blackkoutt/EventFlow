using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Helpers.PdfOptions.PdfCommonOptions
{
    public class CommonOptions
    {
        public CommonOptions() 
        {
            BottomLine = new LineOptions
            {
                PadTop = 10f,
                PadBottom = 10f,
                Width = 1f,
                Unit = Unit.Point
            };
            TopLine = new LineOptions
            {
                PadTop = 10f,
                PadBottom = 10f,
                Width = 1f,
                Unit = Unit.Point
            };
            LeftLine = new LineOptions
            {
                PadLeft = 10f,
                Width = 1f,
                Unit = Unit.Point
            };
            RightLine = new LineOptions
            {
                PadRight = 10f,
                Width = 1f,
                Unit = Unit.Point
            };
        }  
        public LineOptions BottomLine { get; private set; }
        public LineOptions TopLine { get; private set; }
        public LineOptions LeftLine { get; private set; }
        public LineOptions RightLine { get; private set; }
    }
}
