using EventFlowAPI.DB.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Entities
{
    public class MediaPatron : BaseEntity, INameableEntity, ISoftDeleteable, IUpdateableEntity, IPhotoEntity
    {

        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeleteDate { get; set; }
        public bool IsUpdated { get; set; } = false;
        public DateTime? UpdateDate { get; set; }
        public Guid MediaPatronGuid { get; set; }
        public string PhotoName { get; set; } = string.Empty;

        public ICollection<Festival> Festivals { get; set; } = [];
    }
}
