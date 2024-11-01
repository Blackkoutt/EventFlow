namespace EventFlowAPI.DB.Entities.Abstract
{
    public interface ISoftUpdateable
    {
        bool IsSoftUpdated { get; set; }
    }
}
