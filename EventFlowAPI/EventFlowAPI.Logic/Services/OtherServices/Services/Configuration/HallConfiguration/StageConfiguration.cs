using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Helpers.JpgOptions;
using EventFlowAPI.Logic.Helpers.TicketOptions;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces;
using SixLabors.Fonts;
using SixLabors.ImageSharp;

namespace EventFlowAPI.Logic.Services.OtherServices.Services.Configuration.HallConfiguration
{
    public class StageConfiguration(IAssetService assetService)
    {
        
        private readonly IAssetService _assetService = assetService;
        
        private const float stageRectangleThickness = 3f;
        private const int stageLetterWidthInPixels = 22;
        private const int stageLetterHeightInPixels = 30;
        private const int paddingTopSeats = 80;
        private readonly Color stageTextColor = Color.LightSlateGray;
        private readonly Color stageRectangleColor = Color.LightSlateGray;
        private readonly string stageText = $"SCENA ({{0}}m x {{1}}m)";

        private float stageStartX;
        private float stageStartY;
        private float stageOnCanvasWidth;
        private float stageOnCanvasLength;

        public int PaddingTopSeats = paddingTopSeats;
        private Font StageTextFont => _assetService.GetFont(32, FontStyle.Regular, FontType.Inter);

        public OutlineRectanglePrintingOptions? GetStageRectanglePrintingOptions(PointF startPoint, SizeF paintingArea, float? stageWidth, float? stageLength, float hallWidth, float hallLength)
        {
            if (stageWidth is null || stageLength is null) return null;

            var stageToHallWidthRatio = (float)(stageWidth / hallWidth);

            var stageLengthRatio = 4f / 5f;
            var stageToHallLengthRatio = (float)(stageLength / hallLength);

            stageOnCanvasWidth = paintingArea.Width * stageToHallWidthRatio;
            stageOnCanvasLength = (paintingArea.Height * stageToHallLengthRatio) * stageLengthRatio;

            stageStartX = (float)(startPoint.X + (paintingArea.Width - stageOnCanvasWidth) / 2);
            stageStartY = startPoint.Y;

            return new OutlineRectanglePrintingOptions
            {
                Color = stageRectangleColor,
                Thickness = stageRectangleThickness,
                Rectangle = new RectangleF(new PointF(x: stageStartX, y: stageStartY), new SizeF(stageOnCanvasWidth, stageOnCanvasLength))
            };
        }


        public PrintingOptions? GetStageTextPrintingOptions(ref float startPointY, float? stageWidth, float? stageLength)
        {
            if (stageWidth is null || stageLength is null)
            {
                startPointY += paddingTopSeats;
                return null;
            }

            var stageTextToDraw = string.Format(stageText, stageWidth, stageLength);
            var stageTextWidthInPixels = stageTextToDraw.Length * stageLetterWidthInPixels;
            var stageTextHeightInPixels = stageLetterHeightInPixels;

            var stageTextX = (float)(stageStartX + ((stageOnCanvasWidth - stageTextWidthInPixels) / 2));
            var stageTextY = (float)(stageStartY + ((stageOnCanvasLength - stageTextHeightInPixels) / 2));

            startPointY += stageOnCanvasLength + paddingTopSeats;

            return new PrintingOptions
            {
                Font = StageTextFont,
                BrushColor = stageTextColor,
                Location = new PointF(stageTextX, stageTextY)
            };
        }

    }
}
