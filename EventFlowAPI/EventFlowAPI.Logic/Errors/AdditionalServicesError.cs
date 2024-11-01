using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.Response.Abstract;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record AdditionalServicesError(HttpResponse? Details = null)
    {
        public static readonly Error ServiceNotFound = new(new NotFoundResponse("Additional service with given Id does not exist in database."));
        public static readonly Error ServiceDuplicate = new(new BadRequestResponse("Additional services IDs list contains duplicated elements."));
    }
}
