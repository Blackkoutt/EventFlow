using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Helpers.JpgOptions;
using EventFlowAPI.Logic.Helpers.TicketOptions;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces;
using SixLabors.Fonts;
using SixLabors.ImageSharp;

namespace EventFlowAPI.Logic.Services.OtherServices.Services.Configuration.HallConfiguration
{
    public class LegendConfiguration(IAssetService assetService)
    {
        private readonly IAssetService _assetService = assetService;

        // Footer
        private const int footerPadding = 70;
        private const int footerPaddingTop = 20;
        private const int legendItemPadding = 30;
        private const int legendTextPadding = 15;      
        private const int legendTextHeight = 10;       
        private const int colorBlockWidthHeight = 30;
        public int FooterPadding => footerPadding;
        public int LegendTextHeight => legendTextHeight;
        public int LegendTextPadding => legendTextPadding;
        public int ColorBlockWidthHeight => colorBlockWidthHeight;
        public int LegendItemPadding => legendItemPadding;
        public int FooterPaddingTop => footerPaddingTop;

        // Legend
        private float legendX;
        private float legendDeaultX;
        private float legendY;
        private readonly Color legendHeaderColor = Color.Black;
        private readonly Color legendItemColor = Color.Black;
        public float LegendY => legendY;
        private Font LegendFont => _assetService.GetFont(32, FontStyle.Bold, FontType.Inter);
        private Font LegendItemFont => _assetService.GetFont(27, FontStyle.Regular, FontType.Inter);

        public PrintingOptions GetLegendHeaderPrintingOptions(float startPointX, float seatY)
        {
            legendX = startPointX + footerPadding;
            legendDeaultX = legendX;
            legendY = seatY + footerPaddingTop;

            return new PrintingOptions
            {
                Font = LegendFont,
                BrushColor = legendHeaderColor,
                Location = new PointF(legendX, legendY)
            };
        }

        public FillRectanglePrintingOptions GetNonActiveSeatColorBlockPrintingOptions(Color nonActiveSeatColor)
        {
            legendY += legendTextHeight + legendItemPadding;

            return new FillRectanglePrintingOptions
            {
                Color = nonActiveSeatColor,
                Rectangle = new RectangleF(new PointF(legendX, legendY), new SizeF(colorBlockWidthHeight, colorBlockWidthHeight))
            };
        }    

        public FillRectanglePrintingOptions GetActiveSeatColorBlockPrintingOptions(SeatType seatType)
        {
            legendY += legendTextHeight + legendItemPadding;
            legendX = legendDeaultX;

            var drawingColor = System.Drawing.ColorTranslator.FromHtml(seatType.SeatColor);
            Color seatColor = Color.FromRgba(drawingColor.R, drawingColor.G, drawingColor.B, drawingColor.A);
            return new FillRectanglePrintingOptions
            {
                Color = seatColor,
                Rectangle = new RectangleF(new PointF(legendX, legendY), new SizeF(colorBlockWidthHeight, colorBlockWidthHeight))
            };
        }

        public PrintingOptions GetLegendItemDescription()
        {
            legendX += colorBlockWidthHeight + legendTextPadding;

            return new PrintingOptions
            {
                Font = LegendItemFont,
                BrushColor = legendItemColor,
                Location = new PointF(legendX, legendY)
            };
        }
    }
}
