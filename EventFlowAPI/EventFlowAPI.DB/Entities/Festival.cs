using EventFlowAPI.DB.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventFlowAPI.DB.Entities
{
    public class Festival : BaseEntity, INameableEntity, IDateableEntity
    {

        [MaxLength(60)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(300)]
        public string ShortDescription { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime? CancelDate { get; set; }

        [NotMapped]
        public bool IsExpired => EndDate < DateTime.Now;
        public bool IsCanceled { get; set; } = false;

        public FestivalDetails? Details { get; set; }
        public ICollection<Event> Events { get; set; } = [];
        public ICollection<MediaPatron> MediaPatrons { get; set; } = [];
        public ICollection<Organizer> Organizers { get; set; } = [];
        public ICollection<Sponsor> Sponsors { get; set; } = [];
        public ICollection<Ticket> Tickets { get; set; } = [];
    }
}
