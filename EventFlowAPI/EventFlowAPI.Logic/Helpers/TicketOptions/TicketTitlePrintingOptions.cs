using SixLabors.ImageSharp;

namespace EventFlowAPI.Logic.Helpers.TicketOptions
{
    public class TicketTitlePrintingOptions : TicketPrintingOptions
    {
        public string TitleFirstLine = string.Empty;
        public string TitleSecondLine = string.Empty;
        public PointF FirstLineLocation { get; set; }
        public PointF SecondLineLocation { get; set; }
    }
}
