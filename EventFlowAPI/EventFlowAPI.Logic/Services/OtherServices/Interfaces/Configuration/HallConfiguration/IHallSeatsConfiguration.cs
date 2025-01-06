using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Helpers.JpgOptions;
using EventFlowAPI.Logic.Helpers.TicketOptions;
using SixLabors.ImageSharp;

namespace EventFlowAPI.Logic.Services.OtherServices.Interfaces.Configuration.HallConfiguration
{
    public interface IHallSeatsConfiguration
    {
        void CreateWatermark();
        void SetCanvasDimensions(Hall hall, Image logo, bool isDefault = false);
        int CanvasWidth { get; }
        int CanvasHeight { get; }
        Color CanvasBackgroundColor { get; }
        int RowNr { get; }
        int ColNr { get; }
        Image? ResizedLogo { get; }
        OutlineRectanglePrintingOptions CanvasBorderPrintingOptions { get; }
        OutlineRectanglePrintingOptions? GetStageRectanglePrintingOptions(float? stageWidth, float? stageLength, float hallWidth, float hallLength);
        PrintingOptions? GetStageTextPrintingOptions(float? stageWidth, float? stageLength);
        void PrepareToPrintSeats(int maxColumns);
        void SetSeatCursorXDefault();
        void SetSeatCursorToPrintRowNumber();
        PrintingOptions RowColNumberPrintingOptions { get; }
        void SetSeatCursorAfterPrintRowNumber();
        void SetSeatCursorToPrintColNumber();
        void SetSeatCursorAfterPrintColNumber();
        void SetCursorToPrintNextSeatCol();
        void SetCursorToPrintNextSeatsRow();
        OutlineRectanglePrintingOptions GetActiveSeatRectanglePrintingOptions(SeatType? seatType);
        FillRectanglePrintingOptions NonActiveSeatRectanglePrintingOptions { get; }
        PrintingOptions GetSeatNumberPrintingOptions(int seatNr);
        PrintingOptions GetLegendHeaderPrintingOptions();
        FillRectanglePrintingOptions GetNonActiveSeatColorBlockPrintingOptions();
        FillRectanglePrintingOptions GetActiveSeatColorBlockPrintingOptions(SeatType seatType);
        PrintingOptions GetLegendItemDescription();
        ImagePrintingOptions GetWaterMarkPrintingOptions(Image logo);
        string ConvertToRoman(int number);
    }
}
