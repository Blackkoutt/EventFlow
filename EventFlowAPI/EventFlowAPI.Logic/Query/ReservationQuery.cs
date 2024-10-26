using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Query.Abstract;

namespace EventFlowAPI.Logic.Query
{
    public sealed class ReservationQuery : QueryObject, IDateableQuery
    {
        public Status? Status { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? FromReservationDate { get; set; }
        public DateTime? ToReservationDate { get; set; }
        public bool? IsFestivalReservation { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? UserName { get; set; }
        public int? PaymentTypeId { get; set; }
        public bool? ReservationByEventPass { get; set; }
        public int? TicketTypeId { get; set; }
        public decimal? MinSeatsCount { get; set; }
        public decimal? MaxSeatsCount { get; set; }

    }
}
