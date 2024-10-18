namespace EventFlowAPI.DB.Entities.Abstract
{
    public interface ISoftDeleteable
    {
        bool IsCanceled { get; set; }
        DateTime? CancelDate { get; set; }
    }
}
