using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Entities
{
    public class Organizer
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Festival_Organizer> Festivals { get; set; } = [];
    }
}
