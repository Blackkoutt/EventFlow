using EventFlowAPI.Logic.Enums;
using EventFlowAPI.Logic.Query.Abstract;

namespace EventFlowAPI.Logic.Query
{
    public class HallRentQuery : QueryObject, IDateableQuery
    {
        public bool? GetAll { get; set; } = false;
        public Status? Status { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? FromRentDate { get; set; }
        public DateTime? ToRentDate { get; set; }
        public TimeSpan? MinDuration { get; set; }
        public TimeSpan? MaxDuration { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? PaymentTypeId { get; set; }
        public int? HallNr { get; set; }
        public string? UserName { get; set; }
    }
}
