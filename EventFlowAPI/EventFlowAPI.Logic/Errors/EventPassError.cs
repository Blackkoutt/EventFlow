using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.Response.Abstract;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record EventPassError(HttpResponse? Details = null)
    {
        public static readonly Error UserAlreadyHaveActiveEventPass = new(new BadRequestResponse("User already have an active event pass."));
        public static readonly Error UserDoesNotHaveActiveEventPass = new(new BadRequestResponse("User does not have an active event pass."));
        public static readonly Error OnlyOneSeatPerEvent = new(new BadRequestResponse("Event pass allows you to reserve only one seat for one event"));
        public static readonly Error EventPassIsDeleted = new(new BadRequestResponse("Event pass is canceled."));
        public static readonly Error EventPassIsExpired = new(new BadRequestResponse("Event pass is expired."));
        public static readonly Error EventPassExpireBeforeEndOfEvent = new(new BadRequestResponse("Can not make reservation for event because event pass will expire before the event ends."));
        public static readonly Error SessionError = new(new InternalServerErrorResponse("Błąd sesji użytkownika: Cannot found data for transaction and event pass."));
    }
}
