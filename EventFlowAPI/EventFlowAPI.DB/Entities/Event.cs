using EventFlowAPI.DB.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Entities
{
    public class Event : BaseEntity, INameableEntity, IDateableEntity
    {

        [MaxLength(60)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(300)]
        public string ShortDescription { get; set; } = string.Empty;

        public DateTime StartDate { get; set; } 

        public DateTime EndDate { get; set; }
        public TimeSpan Duration { get; set; }
        public int CategoryId { get; set; } 
        public int HallId { get; set; }
        public int DefaultHallId { get; set; }

        public EventCategory Category { get; set; } = default!;
        public EventDetails? Details { get; set; }
        public Hall Hall { get; set; } = default!;
        public ICollection<Festival> Festivals { get; set; } = [];
        public ICollection<Ticket> Tickets { get; set; } = [];
    }
}
