﻿using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Helpers.TicketOptions;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces.TicketConfiguration;
using EventFlowAPI.Logic.Services.OtherServices.Services.TicketConfiguration.Abstract;
using Microsoft.Extensions.Configuration;
using SixLabors.Fonts;
using SixLabors.ImageSharp;

namespace EventFlowAPI.Logic.Services.OtherServices.Services.TicketConfiguration
{
    public class EventTicketConfiguration(IConfiguration configuration) :
        TicketConfiguration<Event>(configuration),
        IEventTicketConfiguration
    {

        private int detailsPrintX = 0;
        private readonly int detailsPrintY = 546;

        // Ticket Tittle Settings 
        protected sealed override short TitleFirstLineY => 230;
        protected sealed override short TitleLineHeight => 20;
        protected sealed override short TitleLeftMax => 43;
        protected sealed override short TitleRightMax => 748;
        protected sealed override short TitleEstimatedCharWidth => 24;
        protected sealed override short TitleEstimatedMaxCharHeight => 35;
        protected sealed override Font TitleFont => GetFont(48, FontStyle.Bold, FontType.Inter_Regular);
        protected sealed override Color TitleColor => Color.Black;


        // Ticket Date Settings 
        protected sealed override Font DateFont => GetFont(40, FontStyle.Bold, FontType.Inter_Regular);
        protected sealed override Color DateColor => Color.Black;
        protected sealed override PointF DateLocation => new(x: 230, y: 426);


        // Ticket Qr Code Settings 
        protected sealed override byte QRCodeSize => 9;
        protected sealed override float QRCodeOpacity => 1f;
        protected sealed override Point QRCodeLocation => new(x: 1607, y: 132);


        // Ticket Price Settings
        private const short priceLeftMax = 73;
        private const short priceRightMax = 256;
        private const short priceEstimatedCharWidth = 12;
        private readonly Color priceColor = Color.Black;
        private const Currency priceCurrency = Currency.PLN;
        private PointF PriceLocation => new(x: detailsPrintX, y: detailsPrintY);
        private Font PriceFont => GetFont(25, FontStyle.Bold, FontType.Inter_Regular);


        // Ticket Hall Settings   
        private readonly Color hallColor = Color.Black;
        private Font HallFont => GetFont(25, FontStyle.Bold, FontType.Inter_Regular);
        private PointF HallLocation => new(x: 371, y: detailsPrintY);


        // Ticket Duration Settings
        private const short durationLeftMax = 507;
        private const short durationRightMax = 717;
        private const short durationEstimatedCharWidth = 12;
        private readonly Color DurationColor = Color.Black;
        private Font DurationFont => GetFont(25, FontStyle.Bold, FontType.Inter_Regular);   
        private PointF DurationLocation => new(x: detailsPrintX, y: detailsPrintY);


        // Ticket Seats Settings
        private readonly Color SeatsColor = Color.Black;
        private Font SeatsFont => GetFont(32, FontStyle.Bold, FontType.Inter_Regular);    
        private PointF SeatsLocation => new(x: 865, y: 85);

        protected override string GetTitle(Event entity) => $"{entity.Category.Name}: {entity.Name}";

        public TicketPricePrintingOptions GetPricePrintingOptions(Reservation reservation)
        {
            int maxSpaceForText = priceRightMax - priceLeftMax;
            int priceStringLength = reservation.Ticket.Price.ToString().Length + 3;
            detailsPrintX = priceLeftMax + (maxSpaceForText - priceStringLength * priceEstimatedCharWidth) / 2;

            return new TicketPricePrintingOptions
            {
                Font = PriceFont,
                BrushColor = priceColor,
                Location = PriceLocation,
                Currency = priceCurrency
            };
        }


        public TicketPrintingOptions GetHallPrintingOptions()
        {
            return new TicketPrintingOptions
            {
                Font = HallFont,
                BrushColor = hallColor,
                Location = HallLocation
            };
        }

        public TicketPrintingOptions GetDurationPrintingOpitons(Event eventEntity)
        {
            int spaceForDuration = durationRightMax - durationLeftMax;
            int durationStringLength = eventEntity.Duration.TotalMinutes.ToString().Length + 4;
            detailsPrintX = durationLeftMax + (spaceForDuration - durationStringLength * durationEstimatedCharWidth) / 2;

            return new TicketPrintingOptions
            {
                Font = DurationFont,
                BrushColor = DurationColor,
                Location = DurationLocation
            };
        }

        public TicketPrintingOptions GetSeatsPrintingOptions()
        {
            return new TicketPrintingOptions
            {
                Font = SeatsFont,
                BrushColor = SeatsColor,
                Location = SeatsLocation
            };
        }
    }
}