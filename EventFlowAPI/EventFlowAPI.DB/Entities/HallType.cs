using EventFlowAPI.DB.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Entities
{
    public class HallType : BaseEntity, INameableEntity
    {

        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(600)]
        public string? Description { get; set; }

        public ICollection<Hall> Halls { get; set; } = [];
        public ICollection<Equipment> Equipments { get; set; } = [];
    }
}
