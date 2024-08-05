using EventFlowAPI.DB.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Entities
{
    public class EventDetails : BaseEntity
    {

        [MaxLength(2000)]
        public string? LongDescription { get; set; }

        public Event? Event { get; set; }
    }
}
