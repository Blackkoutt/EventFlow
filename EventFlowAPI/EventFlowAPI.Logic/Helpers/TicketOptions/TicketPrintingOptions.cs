using SixLabors.Fonts;
using SixLabors.ImageSharp;

namespace EventFlowAPI.Logic.Helpers.TicketOptions
{
    public class TicketPrintingOptions
    {
        public Font Font { get; set; } = default!;
        public Color BrushColor { get; set; }
        public PointF Location { get; set; }
    }
}
