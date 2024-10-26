using EventFlowAPI.DB.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Entities
{
    public class HallType : BaseEntity, INameableEntity, ISoftDeleteable
    {

        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(600)]
        public string? Description { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeleteDate { get; set; }

        public ICollection<Hall> Halls { get; set; } = [];
        public ICollection<Equipment> Equipments { get; set; } = [];
    }
}
