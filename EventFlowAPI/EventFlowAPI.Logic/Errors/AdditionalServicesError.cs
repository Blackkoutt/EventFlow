using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.Response.Abstract;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record AdditionalServicesError(HttpResponse? Details = null)
    {
        public static readonly Error ServiceNotFound = new(new NotFoundResponse("Dodatkowa usługa o podanym ID nie istnieje w bazie danych."));
        public static readonly Error ServiceDuplicate = new(new BadRequestResponse("Lista ID dodatkowych usług zawiera zduplikowane elementy."));
    }
}
