using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.Response.Abstract;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record ReservationError(HttpResponse? Details = null)
    {
        public static readonly Error ReservationListIsEmpty = new(new BadRequestResponse("Could not print ticket because of empty reservation list."));
        public static readonly Error EventIsOutOfDate = new(new BadRequestResponse("Can not make reservation for out of date event."));
    }
}
