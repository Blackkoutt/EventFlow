using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EventFlowAPI.DB.Models
{
    public class Festival
    {
        public int Id { get; set; }

        [MaxLength(40)]
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public FestivalDetails? Details { get; set; }
        public ICollection<Festival_Event> Events { get; set; } = [];
        public ICollection<Festival_MediaPatron> MediaPatrons { get; set; } = [];
        public ICollection<Festival_Organizer> Organizers { get; set; } = [];
        public ICollection<Festival_Sponsor> Sponsors { get; set; } = [];
    }
}
