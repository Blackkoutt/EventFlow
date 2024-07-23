using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Entities
{
    public class MediaPatron
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Festival_MediaPatron> Festivals { get; set; } = [];
    }
}
