namespace EventFlowAPI.Logic.Query.Abstract
{
    public interface IPriceableQuery
    {
        decimal? MinPrice { get; set; }
        decimal? MaxPrice { get; set; }
    }
}
