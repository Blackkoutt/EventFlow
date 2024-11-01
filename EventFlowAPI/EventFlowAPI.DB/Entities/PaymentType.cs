using EventFlowAPI.DB.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Entities
{
    public class PaymentType : BaseEntity, INameableEntity, ISoftDeleteable, IUpdateableEntity, IPhotoEntity
    {

        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeleteDate { get; set; }
        public bool IsUpdated { get; set; } = false;
        public DateTime? UpdateDate { get; set; }
        public Guid PaymentTypeGuid { get; set; }
        public string PhotoName { get; set; } = string.Empty;
        public ICollection<HallRent> HallRents { get; set; } = [];
        public ICollection<EventPass> EventPasses { get; set; } = [];
        public ICollection<Reservation> Reservations { get; set; } = [];
    }
}
