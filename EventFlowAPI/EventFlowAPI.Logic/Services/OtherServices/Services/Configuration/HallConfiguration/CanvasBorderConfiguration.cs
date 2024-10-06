using EventFlowAPI.Logic.Helpers.JpgOptions;
using SixLabors.ImageSharp;

namespace EventFlowAPI.Logic.Services.OtherServices.Services.Configuration.HallConfiguration
{
    public class CanvasBorderConfiguration
    {
        private readonly Color canvasBorderColor = Color.LightSlateGray;
        private const float canvasBorderThickness = 10f;
        private readonly PointF canvasBorderLocation = new PointF(x: 0f, y: 0f);
        private readonly SizeF canvasBorderSize = new SizeF(width: 0f, height: 0f);
        public float CanvasBorderThickness => canvasBorderThickness;

        public OutlineRectanglePrintingOptions CanvasBorderPrintingOptions =>
            new OutlineRectanglePrintingOptions
            {
                Color = canvasBorderColor,
                Thickness = canvasBorderThickness,
                Rectangle = new RectangleF(canvasBorderLocation, canvasBorderSize)
            };
    }
}
