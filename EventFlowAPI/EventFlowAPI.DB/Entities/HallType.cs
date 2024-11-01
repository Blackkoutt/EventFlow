using EventFlowAPI.DB.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Entities
{
    public class HallType : BaseEntity, INameableEntity, IUpdateableEntity, ISoftDeleteable, ISoftUpdateable, IPhotoEntity
    {

        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(600)]
        public string? Description { get; set; }
        public bool IsUpdated { get; set; } = false;
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Guid HallTypeGuid { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool IsSoftUpdated { get; set; } = false;
        public string PhotoName { get; set; } = string.Empty;
        public ICollection<Hall> Halls { get; set; } = [];
        public ICollection<Equipment> Equipments { get; set; } = [];

    }
}
