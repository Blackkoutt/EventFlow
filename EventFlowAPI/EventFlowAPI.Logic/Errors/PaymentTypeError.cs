using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.Response.Abstract;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record PaymentTypeError(HttpResponse? Details = null)
    {
        public static readonly Error PaymentTypeNotFound = new(new BadRequestResponse("Typ płatności o podanym ID nie istnieje w bazie danych."));
    }
}
