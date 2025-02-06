using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.Response.Abstract;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record HallError(HttpResponse? Details = null)
    {
        public static readonly Error HallTypeNotFound = new(new BadRequestResponse("Typ sali o podanym ID nie istnieje w bazie danych."));
        public static readonly Error HallWithSuchNumberExistInDB = new(new BadRequestResponse("Sala o podanym numerze już istnieje w bazie danych."));
        public static readonly Error SeatNumbersInHallAreNotUniqe = new(new BadRequestResponse("Numery miejsc w podanej sali nie są unikalne."));
        public static readonly Error NotFound = new(new BadRequestResponse("Sala o podanym ID nie istnieje w bazie danych."));
        public static readonly Error HallAlreadyExists = new(new BadRequestResponse("Sala o podanym numerze już istnieje w bazie danych."));
        public static readonly Error HallIsTooSmall = new(new BadRequestResponse("Sala nie ma wystarczającej liczby miejsc do wszystkich rezerwacji na wydarzenie."));
        public static readonly Error MappingSeatsError = new(new BadRequestResponse("Błąd podczas mapowania miejsc."));
    }
}
