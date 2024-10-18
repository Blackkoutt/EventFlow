using EventFlowAPI.DB.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventFlowAPI.DB.Entities
{
    public class HallRent : BaseEntity, ISoftDeleteable, IExpireable, IAuthEntity, IDateableEntity
    {
        public Guid HallRentGuid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime? CancelDate { get; set; }

        [NotMapped]
        public bool IsExpired => EndDate < DateTime.Now;
        public bool IsCanceled { get; set; } = false;

        [Range(0.00, 99999.99),
         Column(TypeName = "NUMERIC(7,2)")]
        public decimal PaymentAmount { get; set; }

        public string? HallRentPDFName { get; set; }
        public int PaymentTypeId { get; set; }   
        public int HallId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public PaymentType PaymentType { get; set; } = default!;
        public Hall Hall { get; set; } = default!;
        public User User { get; set; } = default!;
        public ICollection<AdditionalServices> AdditionalServices { get; set; } = [];

    }
}
