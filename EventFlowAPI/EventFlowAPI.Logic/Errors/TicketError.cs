using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.Response.Abstract;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record TicketError(HttpResponse? Details = null)
    {
        public static readonly Error TicketNotFound = new(new BadRequestResponse("Bilet o podanym ID nie istnieje w bazie danych."));
    }
}
