using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Entities
{
    public class EventCategory
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Event> Events { get; set; } = [];
    }
}
