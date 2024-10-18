using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.Response.Abstract;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record MediaPatronError(HttpResponse? Details = null)
    {
        public static readonly Error MediaPatronNotFound = new(new BadRequestResponse("Media patron with given Id does not exist in database."));
    }
}
