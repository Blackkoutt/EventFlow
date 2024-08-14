using EventFlowAPI.DB.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventFlowAPI.DB.Entities
{
    public class HallRent : BaseEntity, IDateableEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime PaymentDate { get; set; }

        [Range(0.00, 99999.99),
         Column(TypeName = "NUMERIC(7,2)")]
        public decimal PaymentAmount { get; set; }   
        public int PaymentTypeId { get; set; }   
        public int HallId { get; set; }   
        public int UserId { get; set; }   

        public PaymentType PaymentType { get; set; } = default!;
        public Hall Hall { get; set; } = default!;
        public User User { get; set; } = default!;
        public ICollection<AdditionalServices> AdditionalServices { get; set; } = [];

    }
}
