using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Query.Abstract;

namespace EventFlowAPI.Logic.Query
{
    public class EventPassQuery : QueryObject
    {
        public EventPassStatus? Status { get; set; }
    }
}
