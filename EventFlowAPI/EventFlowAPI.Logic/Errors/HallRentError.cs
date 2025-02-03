using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.Response.Abstract;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record HallRentError(HttpResponse? Details = null)
    {
        public static readonly Error NotFound = new(new BadRequestResponse("Hall rent with given Id does not exist in database."));
        public static readonly Error HallRentIsDeleted = new(new BadRequestResponse("Can not perform action because hall rent was canceled."));
        public static readonly Error HallRentIsExpired = new(new BadRequestResponse("Can not perform action because hall rent was expired."));
        public static readonly Error CollisionWithExistingEvent = new(new BadRequestResponse("Hall rent has collistion with other existing event. Change hall or start/end date of the rent."));
        public static readonly Error CollisionWithExistingHallRent = new(new BadRequestResponse("Hall rent has collistion with existing hall rent. Change hall or start/end date of the rent."));
        public static readonly Error HallNotFound = new(new BadRequestResponse("Hall with given Id does not exist in database."));
        public static readonly Error HallRentDoesNotExist = new(new BadRequestResponse("Such hall rent does not exist or was canceled some time before."));
        public static readonly Error TooMuchActiveHallRentsInMonth = new(new BadRequestResponse("User have too much active hall rents in this month."));
        public static readonly Error HallRentListIsEmpty = new(new BadRequestResponse("Can not found hall rents to update."));
        public static readonly Error SessionError = new(new InternalServerErrorResponse("Błąd sesji użytkownika: Cannot found data for transaction and hall rent."));
    }
}
