namespace EventFlowAPI.DB.Models
{
    public class Festival_Event
    { 
        public int FestivalId { get; set; }
        public int EventId { get; set; }

        public Festival? Festival { get; set; }
        public Event? Event { get; set; }
    }
}
