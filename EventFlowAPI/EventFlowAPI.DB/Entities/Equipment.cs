using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Entities
{
    public class Equipment
    {
        public int Id { get; set; }

        [MaxLength(40)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Description { get; set; }

        public ICollection<HallType_Equipment> HallTypes { get; set; } = [];
    }
}
