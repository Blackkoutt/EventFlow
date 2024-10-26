namespace EventFlowAPI.DB.Entities.Abstract
{
    public interface ISoftDeleteable
    {
        bool IsDeleted { get; set; }
        DateTime? DeleteDate { get; set; }
    }
}
