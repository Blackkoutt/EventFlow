using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Query.Abstract;

namespace EventFlowAPI.Logic.Query
{
    public class HallRentQuery : QueryObject
    {
        public Status? Status { get; set; }
    }
}
