using EventFlowAPI.Logic.Query.Abstract;

namespace EventFlowAPI.Logic.Query
{
    public class SeatTypeQuery : QueryObject, INameableQuery
    {
        public string? Name { get; set; }
    }
}
