using EventFlowAPI.DB.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Entities
{
    public class TicketType : BaseEntity, INameableEntity, ISoftDeleteable, IUpdateableEntity, ISoftUpdateable
    {

        [MaxLength(40)]
        public string Name { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeleteDate { get; set; }
        public bool IsUpdated { get; set; } = false;
        public DateTime? UpdateDate { get; set; }
        public bool IsSoftUpdated { get; set; } = false;
        public ICollection<Ticket> Tickets { get; set; } = [];
    }
}
