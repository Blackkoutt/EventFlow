using EventFlowAPI.DB.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventFlowAPI.DB.Entities
{
    public class Festival : BaseEntity, ISoftDeleteable, IExpireable, IDateableEntity, INameableEntity
    {

        [MaxLength(60)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(300)]
        public string ShortDescription { get; set; } = string.Empty;
        public DateTime AddDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long Duration { get; set; }
        public DateTime? DeleteDate { get; set; }

        [NotMapped]
        public bool IsExpired => EndDate < DateTime.Now;

        [NotMapped]
        public TimeSpan DurationTimeSpan
        {
            get => TimeSpan.FromSeconds(Duration);
            set => Duration = (long)value.TotalSeconds;
        }

        public bool IsDeleted { get; set; } = false;

        public FestivalDetails? Details { get; set; }
        public ICollection<Event> Events { get; set; } = [];
        public ICollection<MediaPatron> MediaPatrons { get; set; } = [];
        public ICollection<Organizer> Organizers { get; set; } = [];
        public ICollection<Sponsor> Sponsors { get; set; } = [];
        public ICollection<Ticket> Tickets { get; set; } = [];
    }
}
