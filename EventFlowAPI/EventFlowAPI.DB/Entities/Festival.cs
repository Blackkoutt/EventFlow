using EventFlowAPI.DB.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventFlowAPI.DB.Entities
{
    public class Festival : BaseEntity, ISoftDeleteable, IExpireable, IDateableEntity, INameableEntity, IUpdateableEntity, IPhotoEntity
    {

        [MaxLength(60)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(300)]
        public string ShortDescription { get; set; } = string.Empty;
        public DateTime AddDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid FestivalGuid { get; set; }
        public long Duration { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeleteDate { get; set; }
        public bool IsUpdated { get; set; } = false;
        public DateTime? UpdateDate { get; set; }
        public string PhotoName { get; set; } = string.Empty;

        [NotMapped]
        public bool IsExpired => EndDate < DateTime.Now;

        [NotMapped]
        public TimeSpan DurationTimeSpan
        {
            get => TimeSpan.FromSeconds(Duration);
            set => Duration = (long)value.TotalSeconds;
        }
        public FestivalDetails? Details { get; set; }
        public ICollection<Event> Events { get; set; } = [];
        public ICollection<MediaPatron> MediaPatrons { get; set; } = [];
        public ICollection<Organizer> Organizers { get; set; } = [];
        public ICollection<Sponsor> Sponsors { get; set; } = [];
        public ICollection<Ticket> Tickets { get; set; } = [];
    }
}
