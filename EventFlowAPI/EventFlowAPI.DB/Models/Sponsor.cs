using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Models
{
    public class Sponsor
    {
        public int Id { get; set; }

        [MaxLength(25)]
        public string? Name { get; set; }

        public ICollection<Festival_Sponsor> Festivals { get; set; } = new List<Festival_Sponsor>();
    }
}
