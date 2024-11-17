using EventFlowAPI.DB.Entities.Abstract;

namespace EventFlowAPI.DB.Entities
{
    public class News : BaseEntity, IPhotoEntity
    {
        public string Title { get; set; } = string.Empty;
        public Guid NewsGuid { get; set; }
        public DateTime PublicationDate { get; set; }
        public string ShortDescription { get; set; } = string.Empty;
        public string LongDescription { get; set; } = string.Empty;
        public string PhotoName { get; set; } = string.Empty;
    }
}
