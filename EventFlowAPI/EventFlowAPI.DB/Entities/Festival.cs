using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Entities
{
    public class Festival
    {
        public int Id { get; set; }

        [MaxLength(60)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(300)]
        public string ShortDescription { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan Duration { get; set; }

        public FestivalDetails? Details { get; set; }
        public ICollection<Festival_Event> Events { get; set; } = [];
        public ICollection<Festival_MediaPatron> MediaPatrons { get; set; } = [];
        public ICollection<Festival_Organizer> Organizers { get; set; } = [];
        public ICollection<Festival_Sponsor> Sponsors { get; set; } = [];
    }
}
