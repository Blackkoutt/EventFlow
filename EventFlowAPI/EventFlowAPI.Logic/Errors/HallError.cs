using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.Response.Abstract;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record HallError(HttpResponse? Details = null)
    {
        public static readonly Error HallTypeNotFound = new(new BadRequestResponse("Hall type with given Id does not exist in database."));
        public static readonly Error HallWithSuchNumberExistInDB = new(new BadRequestResponse("Hall with such number already exist in database."));
        public static readonly Error SeatNumbersInHallAreNotUniqe = new(new BadRequestResponse("Seat numbers in given hall are not unique."));
        public static readonly Error NotFound = new(new BadRequestResponse("Hall with given Id does not exist in database."));
        public static readonly Error HallAlreadyExists = new(new BadRequestResponse("Hall with given number already exists in database."));
        public static readonly Error HallIsTooSmall = new(new BadRequestResponse("Hall not have enough seats for all event reservations."));
    }
}
