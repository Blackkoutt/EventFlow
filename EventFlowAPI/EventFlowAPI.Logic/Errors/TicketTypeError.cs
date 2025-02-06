using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.Response.Abstract;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record TicketTypeError(HttpResponse? Details = null)
    {
        public static readonly Error NotFound = new(new BadRequestResponse("Typ biletu o podanym ID nie istnieje w bazie danych."));
        public static readonly Error TicketDuplicates = new(new BadRequestResponse("Lista ID typów biletów zawiera zduplikowane elementy."));
        public static readonly Error NormalTicketTypeNotFound = new(new BadRequestResponse("Konieczne jest podanie informacji o normalnym bilecie dla wydarzenia lub festiwalu."));
    }
}
