using SixLabors.ImageSharp;

namespace EventFlowAPI.Logic.Helpers.TicketOptions
{
    public class QRCodePrintingOptions
    {
        public Point Location { get; set; } 
        public float Opacity { get; set; }
        public byte Size { get; set; }
    }
}
