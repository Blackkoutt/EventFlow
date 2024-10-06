using SixLabors.ImageSharp;

namespace EventFlowAPI.Logic.Helpers.JpgOptions
{
    public abstract class RectanglePrintingOptions
    {
        public Color Color { get; set; }
        public RectangleF Rectangle { get; set; }
    }
}
