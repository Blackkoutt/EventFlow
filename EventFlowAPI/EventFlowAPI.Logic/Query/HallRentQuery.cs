using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Query.Abstract;

namespace EventFlowAPI.Logic.Query
{
    public class HallRentQuery : QueryObject
    {
        public HallRentStatus? Status { get; set; }
    }
}
