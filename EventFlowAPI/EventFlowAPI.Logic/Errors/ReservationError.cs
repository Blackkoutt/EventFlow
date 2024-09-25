using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.Response.Abstract;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record ReservationError(HttpResponse? Details = null)
    {
        public static readonly Error ReservationListIsEmpty = new(new BadRequestResponse("Could not print ticket because of empty reservation list."));
        public static readonly Error ReservationDoesNotExist = new(new BadRequestResponse("Such reservaion does not exist or was canceled some time before."));
        public static readonly Error ReservationIsExpired = new(new BadRequestResponse("Can not cancel reservation that has been expired."));
        public static readonly Error EventIsOutOfDate = new(new BadRequestResponse("Can not make reservation for out of date event."));
        public static readonly Error CannotMakeReservationForFestivalOnEventTicket = new(new BadRequestResponse("Can not make reservation for festival because ticket for event was chosen."));
        public static readonly Error CannotMakeReservationForSameEventByEventPass = new(new BadRequestResponse("Can not make reservation becasuse only one reservation per event made by event pass is allowed."));
    }
}
