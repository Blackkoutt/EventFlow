using EventFlowAPI.DB.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventFlowAPI.DB.Entities
{
    public class Event : BaseEntity, ISoftDeleteable, IExpireable, IDateableEntity, INameableEntity
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
        public bool IsDeleted { get; set; } = false;
        public int CategoryId { get; set; } 
        public int HallId { get; set; }
        public EventCategory Category { get; set; } = default!;
        public EventDetails? Details { get; set; }
        public Hall Hall { get; set; } = default!;
        public ICollection<Festival> Festivals { get; set; } = [];
        public ICollection<Ticket> Tickets { get; set; } = [];

        [NotMapped]
        public TimeSpan DurationTimeSpan
        {
            get => TimeSpan.FromSeconds(Duration);
            set => Duration = (long)value.TotalSeconds;
        }
    }
}
