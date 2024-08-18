using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.Response.Abstract;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record SeatError(HttpResponse? Details = null)
    {
        public static readonly Error SeatTypeNotFound = new(new BadRequestResponse("Seat type with given Id does not exist in database."));
        public static readonly Error SeatColumnOutOfRange = new(new BadRequestResponse("Seat column number is greater than the number of seat columns in the hall."));
        public static readonly Error SeatGridColumnOutOfRange = new(new BadRequestResponse("Seat grid column number is greater than the max number of seat columns in the hall."));
        public static readonly Error SeatRowOutOfRange = new(new BadRequestResponse("Seat row number is greater than the number of seat rows in the hall."));
        public static readonly Error SeatGridRowOutOfRange = new(new BadRequestResponse("Seat grid row number is greater than the max number of seat rows in the hall."));
        public static readonly Error NotAvailableSeatChanged = new(new BadRequestResponse("Position or type of not available seat was changed."));

    }
}
