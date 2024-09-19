using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.Response.Abstract;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record SeatError(HttpResponse? Details = null)
    {
        public static readonly Error SeatNotFound = new(new BadRequestResponse("Seat with given Id does not exist in database."));
        public static readonly Error SeatNotAvailable = new(new BadRequestResponse("One of selected seat is not available because it have reservation."));
        public static readonly Error SeatColumnOutOfRange = new(new BadRequestResponse("Seat column number is greater than the number of seat columns in the hall."));
        public static readonly Error SeatGridColumnOutOfRange = new(new BadRequestResponse("Seat grid column number is greater than the max number of seat columns in the hall."));
        public static readonly Error SeatRowOutOfRange = new(new BadRequestResponse("Seat row number is greater than the number of seat rows in the hall."));
        public static readonly Error SeatGridRowOutOfRange = new(new BadRequestResponse("Seat grid row number is greater than the max number of seat rows in the hall."));
        public static readonly Error NotAvailableSeatChanged = new(new BadRequestResponse("Position or type of not available seat was changed."));
        public static readonly Error SeatWithSuchRowAndColumnAlreadyExist = new(new BadRequestResponse("Seat with such row and column number already exists."));
        public static readonly Error OtherSeatExistInSamePosition = new(new BadRequestResponse("Other seat exist in same position in hall."));
        public static readonly Error SeatsDuplicate = new(new BadRequestResponse("Seats list contains duplicated elements."));
    }
}
