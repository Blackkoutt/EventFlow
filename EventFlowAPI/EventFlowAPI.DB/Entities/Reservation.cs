using EventFlowAPI.DB.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventFlowAPI.DB.Entities
{
    public class Reservation : BaseEntity
    {
        public DateTime ReservationDate { get; set; }
        public DateTime StartOfReservationDate { get; set; }
        public DateTime EndOfReservationDate { get; set; }

        [NotMapped]
        public bool IsReservationActive => EndOfReservationDate > DateTime.Now;
        public DateTime PaymentDate { get; set; }

        [Range(0.00, 99999.99),
         Column(TypeName = "NUMERIC(7,2)")]
        public decimal PaymentAmount { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int PaymentTypeId { get; set; }
        public int EventTicketId { get; set; }  
        public User User { get; set; } = default!;
        public PaymentType PaymentType { get; set; } = default!;
        public EventTicket Ticket { get; set; } = default!;
        public ICollection<Seat> Seats { get; set; } = [];
    }
}
