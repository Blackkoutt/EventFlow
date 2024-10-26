using EventFlowAPI.Logic.Query.Abstract;

namespace EventFlowAPI.Logic.Query
{
    public class TicketTypeQuery : QueryObject, INameableQuery
    {
        public string? Name { get; set; }
    }
}
