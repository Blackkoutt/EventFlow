using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Models
{
    public class EventDetails
    {
        public int Id { get; set; }

        [MaxLength(2000)]
        public string? Description { get; set; }

        public Event? Event { get; set; }
    }
}
