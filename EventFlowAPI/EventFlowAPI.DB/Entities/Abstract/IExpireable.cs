namespace EventFlowAPI.DB.Entities.Abstract
{
    public interface IExpireable
    {
        bool IsExpired { get; }
    }
}
