using EventFlowAPI.Logic.Helpers.JpgOptions;
using SixLabors.ImageSharp;

namespace EventFlowAPI.Logic.Services.OtherServices.Services.Configuration.HallConfiguration
{
    public class CanvasBorderConfiguration
    {
        private readonly Color canvasBorderColor = Color.LightSlateGray;
        private const float canvasBorderThickness = 10f;
        private readonly PointF canvasBorderLocation = new PointF(x: 0f, y: 0f);
        public float CanvasBorderThickness => canvasBorderThickness;

        public OutlineRectanglePrintingOptions GetCanvasBorderPrintingOptions(float width, float height) =>
            new OutlineRectanglePrintingOptions
            {
                Color = canvasBorderColor,
                Thickness = canvasBorderThickness,
                Rectangle = new RectangleF(canvasBorderLocation, new SizeF(width, height))
            };
    }
}
