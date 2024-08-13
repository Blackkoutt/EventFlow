using EventFlowAPI.DB.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventFlowAPI.DB.Entities
{
    public class EventTicket : BaseEntity
    {

        [Range(0.00, 999.99),
         Column(TypeName = "NUMERIC(5,2)")]
        public decimal Price { get; set; }
        public int EventId { get; set; }
        public int TicketTypeId { get; set; }
        public Event Event { get; set; } = default!;
        public TicketType TicketType { get; set; } = default!;
        public ICollection<Reservation> Reservations { get; set; } = [];
    }
}
