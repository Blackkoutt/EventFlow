using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.Response.Abstract;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record EventError(HttpResponse? Details = null)
    {
        public static readonly Error CategoryNotFound = new(new BadRequestResponse("Event category with given Id does not exist in database."));
        public static readonly Error HallNotFound = new(new BadRequestResponse("Event hall with given Id does not exist in database."));
        public static readonly Error CollisionWithExistingEvent = new(new BadRequestResponse("Event has collistion with other existing event. Change hall or start/end date of the event."));
        public static readonly Error CollisionWithExistingHallRent = new(new BadRequestResponse("Event has collistion with existing hall rent. Change hall or start/end date of the event."));
        public static readonly Error NotFound = new(new BadRequestResponse("Event with given Id does not exist in database."));
        public static readonly Error EventIsNotInSuchHall = new(new BadRequestResponse("Event with given eventId is not taking place in the hall with given hallId."));
    }
}

