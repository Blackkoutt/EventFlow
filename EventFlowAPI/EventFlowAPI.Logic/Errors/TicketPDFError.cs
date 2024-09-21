using EventFlowAPI.Logic.Response;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record TicketPDFError
    {
        public static readonly Error TicketPDFNotFound = new(new BadRequestResponse("Can not found ticket pdf for given reservation id."));
        public static readonly Error MoreThanOnePDFFound = new(new BadRequestResponse("Error related to the number of PDF files assigned to a given reservation."));
    }
}
