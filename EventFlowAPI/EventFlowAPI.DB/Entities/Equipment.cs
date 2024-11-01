using EventFlowAPI.DB.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Entities
{
    public class Equipment : BaseEntity, INameableEntity, IUpdateableEntity
    {

        [MaxLength(40)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Description { get; set; }
        public bool IsUpdated { get; set; } = false;
        public DateTime? UpdateDate { get; set; }

        public ICollection<HallType> HallTypes { get; set; } = [];
    }
}
