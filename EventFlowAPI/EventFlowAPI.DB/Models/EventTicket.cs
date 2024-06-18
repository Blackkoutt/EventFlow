using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventFlowAPI.DB.Models
{
    public class EventTicket
    {
        public int Id { get; set; }

        [Range(0.00, 999.99),
         Column(TypeName = "NUMERIC(5,2)")]
        public double Price { get; set; }
        public int EventId { get; set; }
        public int TicketTypeId { get; set; }
        public Event? Event { get; set; }
        public TicketType? TicketType { get; set; }
        public ICollection<Reservation> Reservations { get; set; } = [];
    }
}
