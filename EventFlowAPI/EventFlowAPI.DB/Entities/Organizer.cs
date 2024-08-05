using EventFlowAPI.DB.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Entities
{
    public class Organizer : BaseEntity
    {

        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Festival_Organizer> Festivals { get; set; } = [];
    }
}
