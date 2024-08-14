namespace EventFlowAPI.DB.Entities.Abstract
{
    public interface IDateableEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
