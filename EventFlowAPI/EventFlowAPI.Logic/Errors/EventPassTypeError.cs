using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.Response.Abstract;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record EventPassTypeError(HttpResponse? Details = null)
    {
        public static readonly Error EventPassTypeNotFound = new(new BadRequestResponse("Typ karnetu o podanym ID nie istnieje w bazie danych."));
    }
}
