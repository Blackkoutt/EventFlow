using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Models
{
    public class EventCategory
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string? Name { get; set; }

        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
