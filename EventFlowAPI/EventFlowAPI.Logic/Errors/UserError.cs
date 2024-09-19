using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.Response.Abstract;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record UserError(HttpResponse? Details = null)
    {
        public static readonly Error UserNotFound = new(new BadRequestResponse("User with given Id does not exist in database."));
        public static readonly Error UserEmailNotFound = new(new BadRequestResponse("Could not find user email in database."));
    }
}
