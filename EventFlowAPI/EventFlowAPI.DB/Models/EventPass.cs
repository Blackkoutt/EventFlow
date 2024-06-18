using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventFlowAPI.DB.Models
{
    public class EventPass
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? RenewalDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime PaymentDate { get; set; }

        [Range(0.00, 9999.99),
         Column(TypeName = "NUMERIC(6,2)")]
        public double PaymentAmount { get; set; }
        public int PassTypeId { get; set; }
        public int UserId {  get; set; }
        public int PaymentTypeId { get; set; }
        public EventPassType? PassType { get; set; }
        public User? User { get; set; }
        public PaymentType? PaymentType { get; set; }
    }
}
