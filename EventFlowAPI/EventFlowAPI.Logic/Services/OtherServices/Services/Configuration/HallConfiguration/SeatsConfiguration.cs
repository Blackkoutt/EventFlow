using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Enums;
using EventFlowAPI.Logic.Helpers.JpgOptions;
using EventFlowAPI.Logic.Helpers.TicketOptions;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces;
using SixLabors.Fonts;
using SixLabors.ImageSharp;

namespace EventFlowAPI.Logic.Services.OtherServices.Services.Configuration.HallConfiguration
{
    public class SeatsConfiguration(IAssetService assetService)
    {
        private readonly IAssetService _assetService = assetService;

        private const int seatWidth = 55;
        private const int seatHeight = 55;
        private const int seatPaddingRight = 20;
        private const int rowPadding = 20;
        private const int rowColNumberHeight = 26;
        private int rowNr = 1;
        private int colNr = 1;
        private const int seatBorderThickness = 3;
        private const int oneNumberWidth = 11;
        private const int seatNumberHeight = 17;
        private const int rowColNumberWidth = 12;

        private float seatX;
        private float defaultSeatX;
        private float seatY;        
        private int seatsGridAndPaddingWidth;
        private int seatsGridAndPaddingHeight;
        private int colNumberTopFromSeat;
        private int colNrPadding;
        private readonly Color seatNumberColor = Color.Black;
        private readonly Color rowColNumberColor = Color.Black;
        public float SeatY => seatY;
        public int RowColNumberHeight = rowColNumberHeight;
        public int RowPadding => rowPadding;
        public int RowNr => rowNr;
        public int SeatHeight => seatHeight;
        public int ColNr => colNr;
        private Font RowColFont => _assetService.GetFont(20, FontStyle.Bold, FontType.Inter);
        private Font SeatNrFont => _assetService.GetFont(20, FontStyle.Bold, FontType.Inter);
        public Color NonActiveSeatColor => Color.FromRgb(235, 235, 235);

        public void PrepareToPrintSeats(PointF startPoint, SizeF paintingArea, int maxColumns)
        {
            seatsGridAndPaddingWidth = maxColumns * seatWidth + maxColumns * seatPaddingRight;
            seatsGridAndPaddingHeight = maxColumns * seatHeight + maxColumns * rowPadding;
            colNumberTopFromSeat = rowColNumberHeight + 10;
            seatX = startPoint.X + ((paintingArea.Width - seatsGridAndPaddingWidth) / 2);
            defaultSeatX = seatX;
            seatY = startPoint.Y;
        }

        public void SetSeatCursorXDefault() => seatX = defaultSeatX;

        public void SetSeatCursorToPrintRowNumber()
        {
            seatY += (seatHeight - rowColNumberHeight) / 2;
            seatX -= seatWidth;
        }

        public PrintingOptions RowColNumberPrintingOptions => new PrintingOptions
        {
            Font = RowColFont,
            BrushColor = rowColNumberColor,
            Location = new PointF(seatX, seatY)
        };

        public void SetSeatCursorAfterPrintRowNumber()
        {
            rowNr++;
            seatX += seatWidth;
            seatY -= (seatHeight - rowColNumberHeight) / 2;
        }

        public void SetSeatCursorToPrintColNumber()
        {

            seatY -= colNumberTopFromSeat;
            colNrPadding = (seatWidth - (rowColNumberWidth * colNr.ToString().Length)) / 2;
            seatX += colNrPadding; ;
        }

        public void SetSeatCursorAfterPrintColNumber()
        {
            colNr++;
            seatX -= colNrPadding;
            seatY += colNumberTopFromSeat;
        }
        public void SetCursorToPrintNextSeatCol() => seatX += seatWidth + seatPaddingRight;
        public void SetCursorToPrintNextSeatsRow() => seatY += seatHeight + rowPadding;

        public OutlineRectanglePrintingOptions GetActiveSeatRectanglePrintingOptions(SeatType? seatType)
        {
            Color seatColor;
            if (seatType != null)
            {
                var drawingColor = System.Drawing.ColorTranslator.FromHtml(seatType.SeatColor);
                seatColor = Color.FromRgba(drawingColor.R, drawingColor.G, drawingColor.B, drawingColor.A);
            }
            else
            {
                seatColor = NonActiveSeatColor;
            }
            return new OutlineRectanglePrintingOptions
            {
                Color = seatColor,
                Thickness = seatBorderThickness,
                Rectangle = new RectangleF(new PointF(seatX, seatY), new SizeF(seatWidth, seatHeight))
            };
        }
        public FillRectanglePrintingOptions NonActiveSeatRectanglePrintingOptions => new FillRectanglePrintingOptions
        {
            Color = NonActiveSeatColor,
            Rectangle = new RectangleF(new PointF(seatX, seatY), new SizeF(seatWidth, seatHeight))
        };

        public PrintingOptions GetSeatNumberPrintingOptions(int seatNr)
        {
            var seatNumberWidth = oneNumberWidth * seatNr.ToString().Length;
            var seatNumberX = seatX + ((seatWidth - seatNumberWidth) / 2);
            var seatNumberY = seatY + ((seatHeight - seatNumberHeight) / 2);

            return new PrintingOptions
            {
                Font = SeatNrFont,
                BrushColor = seatNumberColor,
                Location = new PointF(seatNumberX, seatNumberY)
            };
        }
    }
}
