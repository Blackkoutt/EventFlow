using EventFlowAPI.DB.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Entities
{
    public class TicketType : BaseEntity
    {

        [MaxLength(40)]
        public string Name { get; set; } = string.Empty;

        public ICollection<EventTicket> Tickets { get; set; } = [];
    }
}
