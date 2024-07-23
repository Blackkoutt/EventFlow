using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Entities
{
    public class TicketType
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;

        public ICollection<EventTicket> Tickets { get; set; } = [];
    }
}
