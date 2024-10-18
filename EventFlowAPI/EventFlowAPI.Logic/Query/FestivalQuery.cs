using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Query.Abstract;

namespace EventFlowAPI.Logic.Query
{
    public class FestivalQuery : QueryObject
    {
        public Status? Status { get; set; }
    }
}
