using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Models
{
    public class Organizer
    {
        public int Id { get; set; }

        [MaxLength(40)]
        public string? Name { get; set; }

        public ICollection<Festival_Organizer> Festivals { get; set; } = new List<Festival_Organizer>();
    }
}
