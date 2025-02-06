using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.Response.Abstract;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record UserError(HttpResponse? Details = null)
    {
        public static readonly Error UserNotFound = new(new BadRequestResponse("Użytkownik o podanym ID nie istnieje w bazie danych."));
        public static readonly Error UserEmailNotFound = new(new BadRequestResponse("Nie można znaleźć adresu e-mail użytkownika w bazie danych."));
    }
}
