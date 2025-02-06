using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.Response.Abstract;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record ReservationError(HttpResponse? Details = null)
    {
        public static readonly Error ReservationListIsEmpty = new(new BadRequestResponse("Nie można wydrukować biletu z powodu pustej listy rezerwacji."));
        public static readonly Error ReservationDoesNotExist = new(new BadRequestResponse("Taka rezerwacja nie istnieje lub została anulowana wcześniej."));
        public static readonly Error ReservationIsExpired = new(new BadRequestResponse("Nie można anulować rezerwacji, która wygasła."));
        public static readonly Error EventIsOutOfDate = new(new BadRequestResponse("Nie można dokonać rezerwacji na wydarzenie, które już się odbyło."));
        public static readonly Error CannotMakeReservationForFestivalOnEventTicket = new(new BadRequestResponse("Nie można dokonać rezerwacji na festiwal, ponieważ wybrano bilet na wydarzenie."));
        public static readonly Error CannotMakeReservationForSameEventByEventPass = new(new BadRequestResponse("Nie można dokonać rezerwacji, ponieważ możliwa jest jedna rezerwacja na karnet."));
        public static readonly Error ReservationIsDeleted = new(new BadRequestResponse("Rezerwacja została anulowana."));
    }
}
