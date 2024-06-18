using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EventFlowAPI.DB.Models
{
    public class Event
    {
        public int Id { get; set; }

        [NotNull]
        [MaxLength(60)]
        public string? Name { get; set; }

        public DateTime StartDate { get; set; } 

        public DateTime EndDate { get; set; }
        public int CategoryId { get; set; } 
        public int HallNr { get; set; }

        public EventCategory? Category { get; set; } 
        public EventDetails? Details { get; set; }
        public Hall? Hall { get; set; }
        public ICollection<Festival_Event> Festivals { get; set; } = new List<Festival_Event>();
        public ICollection<EventTicket> Tickets { get; set; } = new List<EventTicket>();
    }
}
