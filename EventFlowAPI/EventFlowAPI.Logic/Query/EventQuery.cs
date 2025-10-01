using EventFlowAPI.Logic.Enums;
using EventFlowAPI.Logic.Query.Abstract;

namespace EventFlowAPI.Logic.Query
{
    public class EventQuery : QueryObject, INameableQuery, IDateableQuery
    {
        public Status? Status { get; set; }
        public string? Name { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public TimeSpan? MinDuration { get; set; }
        public TimeSpan? MaxDuration { get; set; }
        public int? CategoryId { get; set; }
        public int? HallNr { get; set; }
        public int? TicketTypeId { get; set; }
        public bool? IsFestivalEvent { get; set; }
        public decimal? MinTicketPrice { get; set; }
        public decimal? MaxTicketPrice { get; set; }
    }
}
