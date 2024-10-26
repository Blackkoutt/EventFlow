using EventFlowAPI.Logic.Query.Abstract;

namespace EventFlowAPI.Logic.Query
{
    public class EventCategoryQuery : QueryObject, INameableQuery
    {
        public string? Name { get; set; }
    }
}
