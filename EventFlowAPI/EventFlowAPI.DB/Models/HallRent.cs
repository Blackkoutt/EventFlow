using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Models
{
    public class HallRent
    {
        public int Id { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime PaymentDate { get; set; }

        [Range(0.00, 99999.99)]
        public double PaymentAmount { get; set; }   
        public int PaymentTypeId { get; set; }   
        public int HallNr { get; set; }   
        public int UserId { get; set; }   

        public PaymentType? PaymentType { get; set; }
        public Hall? Hall { get; set; }  
        public User? User { get; set; }
        public ICollection<HallRent_AdditionalServices> AdditionalServices { get; set; } = new List<HallRent_AdditionalServices>();

    }
}
