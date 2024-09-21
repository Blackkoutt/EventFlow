using EventFlowAPI.Logic.Helpers.Enums;

namespace EventFlowAPI.Logic.Query.Abstract
{
    public class QueryObject
    {
        
        public string? SortBy { get; set; }
        public SortDirection SortDirection { get; set; } = SortDirection.ASC;
    }
}
