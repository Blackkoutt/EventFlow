using EventFlowAPI.DB.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Entities
{
    public class EventCategory : BaseEntity
    {

        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Event> Events { get; set; } = [];
    }
}
