using EventFlowAPI.DB.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventFlowAPI.DB.Entities
{
    public class Reservation : BaseEntity
    {
        public Guid ReservationGuid { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime StartOfReservationDate { get; set; }
        public DateTime EndOfReservationDate { get; set; }

        [NotMapped]
        public bool IsReservationActive => EndOfReservationDate > DateTime.Now;
        public DateTime PaymentDate { get; set; }

        [Range(0.00, 999.99),
         Column(TypeName = "NUMERIC(5,2)")]
        public decimal TotalAddtionalPaymentPercentage { get; set; }

        [Range(0.00, 9999.99),
        Column(TypeName = "NUMERIC(6,2)")]
        public decimal TotalAdditionalPaymentAmount { get; set; }

        [Range(0.00, 99999.99),
         Column(TypeName = "NUMERIC(7,2)")]
        public decimal PaymentAmount { get; set; }

        public string UserId { get; set; } = string.Empty;
        public int PaymentTypeId { get; set; }
        public int TicketId { get; set; }  
        public User User { get; set; } = default!;
        public PaymentType PaymentType { get; set; } = default!;
        public Ticket Ticket { get; set; } = default!;
        public ICollection<Seat> Seats { get; set; } = [];
    }
}
