using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.Response.Abstract;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record MediaPatronError(HttpResponse? Details = null)
    {
        public static readonly Error MediaPatronNotFound = new(new BadRequestResponse("Patron medialny o podanym ID nie istnieje w bazie danych."));
    }
}
