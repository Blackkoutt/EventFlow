using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Models
{
    public class EventPassType
    {
        public int Id { get; set; }

        [MaxLength(40)]
        public string? Name { get; set; }

        [Range(0.00, 9999.99)]
        public double Price { get; set; }

        public ICollection<EventPass> EventPasses { get; set; } = new List<EventPass>();
    }
}
