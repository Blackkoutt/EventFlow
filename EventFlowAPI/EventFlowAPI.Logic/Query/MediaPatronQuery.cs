using EventFlowAPI.Logic.Query.Abstract;

namespace EventFlowAPI.Logic.Query
{
    public class MediaPatronQuery : QueryObject, INameableQuery
    {
        public string? Name { get; set; }
    }
}
