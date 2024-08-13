using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.Response.Abstract;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record EventError(HttpResponse? Details = null)
    {
        public static readonly Error CategoryNotFound = new(new BadRequestResponse("Event category with given Id does not exist in database."));
        public static readonly Error HallNotFound = new(new BadRequestResponse("Event hall with given Id does not exist in database."));
    }
}

