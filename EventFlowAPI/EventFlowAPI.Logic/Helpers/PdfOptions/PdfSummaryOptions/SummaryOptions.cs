using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfTextOptions;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Helpers.PdfOptions.PdfSummaryOptions
{
    public class SummaryOptions
    {
        private readonly Reservation _reservation;
        private readonly string defaultFontType = FontType.Inter.ToString();

        public SummaryOptions(Reservation reservation, List<SeatType> seatTypes, List<TicketType> ticketTypes)
        {
            _reservation = reservation;
            AdditionalPayment = new AdditionalPaymentOptions(seatTypes);
            TicketType = new TicketTypeOptions(ticketTypes);
        }

        // Summary Container
        public float ContainerPadLeft => 10f;
        public float ContainerRowSpacing => 5f;
        public float ContainerSummaryDescriptionRowWidth => 1f;
        public float ContainerSummaryRowWidth => 1f;


        // Summary Description
        public float DescriptionAdditionalPaymentRowWidth => 1.5f;
        public float DescriptionTicketTypesRowWidth => 1f;


        // Additional Payments   
        public AdditionalPaymentOptions AdditionalPayment { get; private set; }

        // Ticket Types
        public TicketTypeOptions TicketType { get; private set; }

        // Summary
        public float PadRight => 10f;
        public float ColumnSpacing => 5f;
        public float ItemPadVertical => 4f;
        public float ItemPadHorizontal => 8f;
        public SummaryTextOptions TicketPrice => new SummaryTextOptions
        {
            Label = "Cena biletu",
            Value = $"{_reservation.Ticket.Price} {Currency.PLN}",
            TextBackgound = "#ededed",
            Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(11f),
            PadHorizontal = 8,
            PadVertical = 4,
            LabelWidth = 2,
            ValueWidth = 1
        };
        public SummaryTextOptions SeatsCount => new SummaryTextOptions
        {
            Label = "Ilość miejsc",
            Value = $"{_reservation.Seats.Count}",
            TextBackgound = "#ededed",
            Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(11f),
            PadHorizontal = 8,
            PadVertical = 4,
            LabelWidth = 2,
            ValueWidth = 1
        };
        public SummaryTextOptions AdditionalPayments => new SummaryTextOptions
        {
            Label = $"Dodatkowe opłaty (+{_reservation.TotalAddtionalPaymentPercentage}%)",
            Value = $"+ {_reservation.TotalAdditionalPaymentAmount} {Currency.PLN}",
            TextBackgound = "#ededed",
            Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(11f),
            PadHorizontal = 8,
            PadVertical = 4,
            LabelWidth = 2,
            ValueWidth = 1
        };
        public SummaryTextOptions TotalCost => new SummaryTextOptions
        {
            Label = "SUMA",
            Value = $"{_reservation.PaymentAmount} {Currency.PLN}",
            TextBackgound = "#ffffff",
            Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(12f).SemiBold(),
            PadHorizontal = 8,
            PadVertical = 4,
            LabelWidth = 2,
            ValueWidth = 1
        };
    }
}
