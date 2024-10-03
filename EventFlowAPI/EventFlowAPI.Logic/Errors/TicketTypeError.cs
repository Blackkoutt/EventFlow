using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.Response.Abstract;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record TicketTypeError(HttpResponse? Details = null)
    {
        public static readonly Error NotFound = new(new BadRequestResponse("Ticket type with given Id does not exist in database."));
    }
}
