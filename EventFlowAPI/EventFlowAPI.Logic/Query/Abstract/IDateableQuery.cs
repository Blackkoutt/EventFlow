namespace EventFlowAPI.Logic.Query.Abstract
{
    public interface IDateableQuery
    {
        DateTime? FromDate { get; set; }
        DateTime? ToDate { get; set; }
    }
}
