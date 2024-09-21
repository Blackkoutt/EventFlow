using EventFlowAPI.Logic.Response;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record TicketJPGError
    {
        public static readonly Error TicketJPGNotFound = new(new BadRequestResponse("Can not found any ticket jpg for given reservation id."));
    }
}
