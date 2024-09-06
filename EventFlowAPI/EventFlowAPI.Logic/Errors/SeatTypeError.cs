using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.Response.Abstract;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record SeatTypeError(HttpResponse? Details = null)
    {
        public static readonly Error SeatTypeNotFound = new(new BadRequestResponse("Seat type with given Id does not exist in database."));
    }
}
