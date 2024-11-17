using EventFlowAPI.DB.Entities.Abstract;

namespace EventFlowAPI.DB.Entities
{
    public class Partner : BaseEntity, IPhotoEntity
    {
        public string Name { get; set; } = string.Empty;
        public Guid PartnerGuid { get; set; }
        public string PhotoName { get; set; } = string.Empty;
    }
}
