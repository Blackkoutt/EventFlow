namespace EventFlowAPI.DB.Entities.Abstract
{
    public interface IDateableEntity
    {
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
    }
}
