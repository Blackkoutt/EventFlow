using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.Response.Abstract;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record TicketTypeError(HttpResponse? Details = null)
    {
        public static readonly Error NotFound = new(new BadRequestResponse("Ticket type with given Id does not exist in database."));
        public static readonly Error TicketDuplicates = new(new BadRequestResponse("Ticket type IDs list contains duplicated elements."));
        public static readonly Error NormalTicketTypeNotFound = new(new BadRequestResponse("It is essential to provide information about normal ticket for event or festival."));
    }
}
