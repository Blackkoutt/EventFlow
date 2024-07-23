using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Entities
{
    public class Event
    {
        public int Id { get; set; }

        [MaxLength(60)]
        public string Name { get; set; } = string.Empty;

        public DateTime StartDate { get; set; } 

        public DateTime EndDate { get; set; }
        public int CategoryId { get; set; } 
        public int HallNr { get; set; }

        public EventCategory? Category { get; set; } 
        public EventDetails? Details { get; set; }
        public Hall? Hall { get; set; }
        public ICollection<Festival_Event> Festivals { get; set; } = [];
        public ICollection<EventTicket> Tickets { get; set; } = [];
    }
}
