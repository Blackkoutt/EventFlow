using EventFlowAPI.DB.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Entities
{
    public class HallType : BaseEntity
    {

        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(600)]
        public string? Description { get; set; }

        public ICollection<Hall> Halls { get; set; } = [];
        public ICollection<HallType_Equipment> Equipments { get; set; } = [];
    }
}
