using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Models
{
    public class TicketType
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string? Name { get; set; }

        public ICollection<EventTicket> Tickets { get; set; } = new List<EventTicket>();
    }
}
