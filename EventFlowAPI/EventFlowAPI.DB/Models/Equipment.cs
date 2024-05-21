using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Models
{
    public class Equipment
    {
        public int Id { get; set; }

        [MaxLength(40)]
        public string? Name { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        public ICollection<HallType_Equipment> HallTypes { get; set; } = new List<HallType_Equipment>();
    }
}
