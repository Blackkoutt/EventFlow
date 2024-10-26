using EventFlowAPI.Logic.Query.Abstract;

namespace EventFlowAPI.Logic.Query
{
    public class OrganizerQuery : QueryObject, INameableQuery
    {
        public string? Name { get; set; }
    }
}
