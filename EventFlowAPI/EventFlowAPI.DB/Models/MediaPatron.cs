using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Models
{
    public class MediaPatron
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string? Name { get; set; }

        public ICollection<Festival_MediaPatron> Festivals { get; set; } = new List<Festival_MediaPatron>();
    }
}
