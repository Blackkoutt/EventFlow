using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EventFlowAPI.DB.Models
{
    public class HallType
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(600)]
        public string? Description { get; set; }

        public ICollection<Hall> Halls { get; set; } = [];
        public ICollection<HallType_Equipment> Equipments { get; set; } = [];
    }
}
