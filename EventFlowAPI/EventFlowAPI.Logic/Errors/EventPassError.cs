using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.Response.Abstract;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record EventPassError(HttpResponse? Details = null)
    {
        public static readonly Error UserAlreadyHaveActiveEventPass = new(new BadRequestResponse("User already have an active event pass."));
        public static readonly Error EventPassIsCanceled = new(new BadRequestResponse("Event pass is canceled."));
        public static readonly Error EventPassIsExpired = new(new BadRequestResponse("Event pass is expired."));
    }
}
