using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Enums;
using EventFlowAPI.Logic.Helpers;
using EventFlowAPI.Logic.Helpers.TicketOptions;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces.Configuration.TicketConfiguration;
using EventFlowAPI.Logic.Services.OtherServices.Services.Configuration.TicketConfiguration.Abstract;
using SixLabors.Fonts;
using SixLabors.ImageSharp;

namespace EventFlowAPI.Logic.Services.OtherServices.Services.Configuration.TicketConfiguration
{
    public class FestivalTicketConfiguration :
        TicketConfiguration<Festival>,
        IFestivalTicketConfiguration
    {
        // Reverse Ticket Constant Settings
        private const short firstColumnX_DefaultValue = 82;
        private const short y_DefaultValue = 137;
       
        private const short tabsCount = 6;
        private const short secondColumnX_DefaultValue = 1017;
        private const short nextTabY = 180;
        private const short numberWidthAndMarginRight = 92;
        private const short yFromNumberToEventInfo = -7;
        private const short yFromEventInfoToSeats = 38;


        // Front Ticket Tittle Settings 
        protected sealed override short TitleFirstLineY => 126;
        protected sealed override short TitleLineHeight => 35;
        protected sealed override short TitleLeftMax => 682;
        protected sealed override short TitleRightMax => 1600;
        protected sealed override short TitleEstimatedCharWidth => 39;
        protected sealed override short TitleEstimatedMaxCharHeight => 104;
        protected sealed override Font TitleFont => _assetService.GetFont(130, FontStyle.Bold, FontType.BigShouldersDisplay);
        protected sealed override Color TitleColor => Color.FromRgb(255, 102, 196);


        // Front Ticket Date Settings 
        protected sealed override Font DateFont => _assetService.GetFont(60, FontStyle.Regular, FontType.BigShouldersDisplay);
        protected sealed override Color DateColor => Color.Black;
        protected sealed override PointF DateLocation => new(x: 995, y: 470);
        protected sealed override string FormatDate => DateFormat.Date;


        // Front Ticket Qr Code Settings 
        protected sealed override byte QRCodeSize => 8;
        protected sealed override float QRCodeOpacity => 1f;
        protected sealed override Point QRCodeLocation => new Point(1645, 160);


        // Reverse Ticket Tab Number Settings
        private Font TabNumberFont => _assetService.GetFont(55, FontStyle.Bold, FontType.Inter);
        private Color TabNumberColor => Color.Black;
        private PointF TabNumberLocation => new(x: printX, y: printY);


        // Reverse Ticket Tab Number Settings
        private Font EventInfoFont => _assetService.GetFont(24, FontStyle.Regular, FontType.Inter);
        private Color EventInfoColor => Color.Black;
        private PointF EventInfoLocation => new(x: printX, y: printY);


        // Reverse Ticket Seats Settings
        private Font SeatsFont => _assetService.GetFont(30, FontStyle.Bold, FontType.Inter);
        private Color SeatsColor => Color.Black;
        private PointF SeatsLocation => new(x: printX, y: printY);


        private int tabStartX;
        private int tabStartY;
        private int printX;
        private int printY;

        public FestivalTicketConfiguration(IAssetService assetService) : base(assetService)
        {
            tabStartX = firstColumnX_DefaultValue;
            tabStartY = y_DefaultValue;
            printX = tabStartX;
            printY = tabStartY;
        }

        public void SetDefaultPrintingParams()
        {
            tabStartX = firstColumnX_DefaultValue;
            tabStartY = y_DefaultValue;
            printX = tabStartX;
            printY = tabStartY;
        }

        public PrintingOptions GetTabNumberPrintingOptions()
        {
            return new PrintingOptions
            {
                Font = TabNumberFont,
                BrushColor = TabNumberColor,
                Location = TabNumberLocation
            };
        }

        public PrintingOptions GetEventInfoPrintingOpitons()
        {
            printX += numberWidthAndMarginRight;
            printY += yFromNumberToEventInfo;

            return new PrintingOptions
            {
                Font = EventInfoFont,
                BrushColor = EventInfoColor,
                Location = EventInfoLocation
            };
        }

        public PrintingOptions GetSeatsPrintingOpitons()
        {
            printY += yFromEventInfoToSeats;

            return new PrintingOptions
            {
                Font = SeatsFont,
                BrushColor = SeatsColor,
                Location = SeatsLocation
            };
        }

        public short TabsCount
        {
            get { return tabsCount; }
        }

        public void MoveCursorToNextTab(int tabNr)
        {
            tabStartY += nextTabY;
            if (tabNr % 3 == 0)
            {
                if (tabStartX == firstColumnX_DefaultValue)
                {
                    tabStartX = secondColumnX_DefaultValue;
                }
                else if (tabStartX == secondColumnX_DefaultValue)
                {
                    tabStartX = firstColumnX_DefaultValue;
                }
                tabStartY = y_DefaultValue;
            }
            printX = tabStartX;
            printY = tabStartY;
        }

        public bool IsNextPageOfReverseTicket()
        {
            if (printX == firstColumnX_DefaultValue && printY == y_DefaultValue)
            {
                return true;
            }
            return false;
        }
        protected override string GetTitle(Festival entity) => entity.Name.ToUpper();
        public int GetReverseTicketCount(int reservationsCount) => reservationsCount / tabsCount + 1;
        public string GetEventInfo(Event eventEntity)
        {
            return $"{eventEntity.Category.Name}: {eventEntity.Name}, " +
                   $"{eventEntity.StartDate.ToString(DateFormat.DateTime)}, " +
                   $"Sala {eventEntity.Hall.HallNr}";
        }
        public string GetSeatsString(Reservation reservation)
        {
            return $"Miejsca: {string.Join(",", reservation.Seats.Select(s => s.SeatNr))}";
        }

    }
}
