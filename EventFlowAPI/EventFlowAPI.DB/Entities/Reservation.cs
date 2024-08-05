using EventFlowAPI.DB.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventFlowAPI.DB.Entities
{
    public class Reservation : BaseEntity
    {
        public DateTime ReservationDate { get; set; }
        public DateTime PaymentDate { get; set; }

        [Range(0.00, 99999.99),
         Column(TypeName = "NUMERIC(7,2)")]
        public decimal PaymentAmount { get; set; }
        public int UserId { get; set; }
        public int PaymentTypeId { get; set; }
        public int EventTicketId { get; set; }  
        public User? User { get; set; }
        public PaymentType? PaymentType { get; set; }
        public EventTicket? Ticket { get; set; }
        public ICollection<Reservation_Seat> Seats { get; set; } = [];
    }
}
