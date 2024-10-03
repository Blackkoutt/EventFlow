using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Query.Abstract;

namespace EventFlowAPI.Logic.Query
{
    public class EventQuery : QueryObject
    {
        public EventStatus? Status { get; set; }
    }
}
