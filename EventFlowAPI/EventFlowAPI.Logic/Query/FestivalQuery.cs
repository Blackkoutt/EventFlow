using EventFlowAPI.Logic.Enums;
using EventFlowAPI.Logic.Query.Abstract;

namespace EventFlowAPI.Logic.Query
{
    public class FestivalQuery : QueryObject, INameableQuery, IDateableQuery
    {
        public Status? Status { get; set; }
        public string? Name { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public TimeSpan? MinDuration { get; set; }
        public TimeSpan? MaxDuration { get; set; }
        public int? MinEventsCount { get; set; }
        public int? MaxEventsCount { get; set; }
        public int? MediaPatronId { get; set; }
        public int? OrganizerId { get; set; }
        public int? SponsorId { get; set; }
        public int? TicketTypeId { get; set; }
        public decimal? MinTicketPrice { get; set; }
        public decimal? MaxTicketPrice { get; set; }

    }
}
