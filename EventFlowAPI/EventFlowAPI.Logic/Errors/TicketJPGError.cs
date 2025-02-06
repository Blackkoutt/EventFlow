using EventFlowAPI.Logic.Response;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record TicketJPGError
    {
        public static readonly Error TicketJPGNotFound = new(new BadRequestResponse("Nie można znaleźć pliku JPG biletu dla podanego ID rezerwacji."));
    }
}
