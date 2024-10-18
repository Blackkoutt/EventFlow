using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.Response.Abstract;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record SponsorError(HttpResponse? Details = null)
    {
        public static readonly Error SponsorNotFound = new(new BadRequestResponse("Sponsor with given Id does not exist in database."));
    }
}
