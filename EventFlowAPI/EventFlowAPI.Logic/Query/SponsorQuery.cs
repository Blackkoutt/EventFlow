using EventFlowAPI.Logic.Query.Abstract;

namespace EventFlowAPI.Logic.Query
{
    public class SponsorQuery : QueryObject, INameableQuery
    {
        public string? Name { get; set; }
    }
}
