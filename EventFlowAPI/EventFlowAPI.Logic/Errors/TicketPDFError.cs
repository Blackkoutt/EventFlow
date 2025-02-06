using EventFlowAPI.Logic.Response;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record TicketPDFError
    {
        public static readonly Error TicketPDFNotFound = new(new BadRequestResponse("Nie można znaleźć pliku PDF biletu dla podanego ID rezerwacji."));
        public static readonly Error MoreThanOnePDFFound = new(new BadRequestResponse("Błąd związany z liczbą plików PDF przypisanych do danej rezerwacji."));
    }
}
