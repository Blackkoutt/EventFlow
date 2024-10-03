using EventFlowAPI.DB.Entities;

namespace EventFlowAPI.Logic.Helpers
{
    public class OldEventInfo
    {
        public string? Name { get; set; } = null;
        public int? HallNr { get; set; } = null;
        public DateTime? StartDate { get; set; } = null;
        public DateTime? EndDate { get; set; } = null;
        public string? CategoryName { get; set; } = null;
        public bool SendMailAboutUpdatedEvent { get; set; } = false;
        public IEnumerable<Reservation> ReservationList { get; set; } = [];

    }
}
