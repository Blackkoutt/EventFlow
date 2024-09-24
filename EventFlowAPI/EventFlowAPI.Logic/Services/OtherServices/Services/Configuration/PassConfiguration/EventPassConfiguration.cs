using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Helpers;
using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Helpers.TicketOptions;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces.Configuration.PassConfiguration;
using SixLabors.Fonts;
using SixLabors.ImageSharp;

namespace EventFlowAPI.Logic.Services.OtherServices.Services.Configuration.PassConfiguration
{
    public class EventPassConfiguration (IAssetService assetService) : IEventPassConfiguration
    {
        private readonly IAssetService _assetService = assetService;

        // EventPass Type
        private readonly Color EventPassTypeColor = Color.Black;
        private Font EventPassTypeFont => _assetService.GetFont(45, FontStyle.Regular, FontType.OpenSansCondensed);
        private PointF EventPassTypeLocation => new(x: 691, y: 334);


        // EventPass Owner
        private readonly Color EventPassOwnerColor = Color.Black;
        private Font EventPassOwnerFont => _assetService.GetFont(45, FontStyle.Regular, FontType.OpenSansCondensed);
        private PointF EventPassOwnerLocation => new(x: 691, y: 412);


        // EventPass Data
        private readonly Color EventPassDateColor = Color.Black;
        private Font EventPassDateFont => _assetService.GetFont(45, FontStyle.Regular, FontType.OpenSansCondensed);
        private PointF EventPassDateLocation => new(x: 691, y: 492);
        private string EventPassDateFormat => DateFormat.Date;


        // EventPass QrCode
        private Point EventPassQRCodeLocation => new(x: 1383, y: 92);
        private float EventPassQRCodeOpacity = 1f;
        private byte EventPassQRCodeSize = 11;

        public PrintingOptions EventPassTypePrintingOptions => new()
        {
            Font = EventPassTypeFont,
            BrushColor = EventPassTypeColor,
            Location = EventPassTypeLocation
        };

        public PrintingOptions EventPassOwnerPrintingOptions => new()
        {
            Font = EventPassOwnerFont,
            BrushColor = EventPassOwnerColor,
            Location = EventPassOwnerLocation,
        };

        public DatePrintingOptions EventPassDatePrintingOptions => new()
        {
            Font = EventPassDateFont,
            BrushColor = EventPassDateColor,
            Location = EventPassDateLocation,
            DateFormat = EventPassDateFormat
        };

        public QRCodePrintingOptions EventPassQrCodePrintingOptions => new()
        {
            Location = EventPassQRCodeLocation,
            Opacity = EventPassQRCodeOpacity,
            Size = EventPassQRCodeSize
        };
    }
}
