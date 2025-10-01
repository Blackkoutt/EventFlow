using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Enums;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfTextOptions;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace EventFlowAPI.Logic.Helpers.PdfOptions.PdfSummaryOptions
{
    public class ReservationSummaryOptions : SummaryOptions
    {
        private readonly Reservation _reservation;

        public ReservationSummaryOptions(Reservation reservation, List<SeatType> seatTypes, List<Ticket> tickets)
        {
            _reservation = reservation;
            AdditionalPayment = new AdditionalPaymentOptions(seatTypes);
            TicketType = new TicketTypeOptions(tickets);
        }

        public Reservation Reservation => _reservation;

        // Summary Description
        public float DescriptionAdditionalPaymentRowWidth => 1.5f;
        public float DescriptionTicketTypesRowWidth => 1f;


        // Additional Payments   
        public AdditionalPaymentOptions AdditionalPayment { get; private set; }

        // Ticket Types
        public TicketTypeOptions TicketType { get; private set; }

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

        public SummaryTextOptions EventPassDiscount => new SummaryTextOptions
        {
            Label = $"Aktywny karnet EventFlow",
            Value = $"- {Math.Round(_reservation.TotalDiscount, 2)} {Currency.PLN}",
            TextBackgound = "#ededed",
            Style = TextStyle.Default.FontFamily(defaultFontType).FontSize(11f),
            PadHorizontal = 8,
            PadVertical = 4,
            LabelWidth = 2,
            ValueWidth = 1
        };

        protected sealed override string PriceLabel => "Cena biletu";

        protected sealed override string PriceValue => $"{_reservation.Ticket.Price} {Currency.PLN}";

        protected sealed override string TotalCostValue => $"{_reservation.PaymentAmount} {Currency.PLN}";
    } 
}
