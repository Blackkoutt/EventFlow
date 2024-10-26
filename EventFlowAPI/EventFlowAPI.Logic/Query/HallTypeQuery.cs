using EventFlowAPI.Logic.Query.Abstract;

namespace EventFlowAPI.Logic.Query
{
    public class HallTypeQuery : QueryObject, INameableQuery
    {
        public string? Name { get; set; }
    }
}
