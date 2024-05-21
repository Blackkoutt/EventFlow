using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Models
{
    public class HallType
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string? Name { get; set; }

        [MaxLength(600)]
        public string? Description { get; set; }

        public ICollection<Hall> Halls { get; set; } = new List<Hall>();
        public ICollection<HallType_Equipment> Equipment { get; set; } = new List<HallType_Equipment>();
    }
}
