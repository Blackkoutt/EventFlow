using EventFlowAPI.Logic.Enums;

namespace EventFlowAPI.Logic.Query.Abstract
{
    public class QueryObject
    {
        public string? SortBy { get; set; }
        public SortDirection SortDirection { get; set; } = SortDirection.ASC;
        public int? PageNumber { get; set; }    
        public int? PageSize { get; set;}
    }
}
