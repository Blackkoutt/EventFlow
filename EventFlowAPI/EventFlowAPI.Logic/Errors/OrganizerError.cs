using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.Response.Abstract;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record OrganizerError(HttpResponse? Details = null)
    {
        public static readonly Error OrganizerNotFound = new(new BadRequestResponse("Organizator o podanym ID nie istnieje w bazie danych."));
    }
}
