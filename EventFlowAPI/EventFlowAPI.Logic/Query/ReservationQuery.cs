using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Query.Abstract;

namespace EventFlowAPI.Logic.Query
{
    public sealed class ReservationQuery : QueryObject
    {
        public ReservationStatus? Status { get; set; }
    }
}
