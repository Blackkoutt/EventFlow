using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.Response.Abstract;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record TicketError(HttpResponse? Details = null)
    {
        public static readonly Error TicketNotFound = new(new BadRequestResponse("Ticket with given Id does not exist in database."));
    }
}
