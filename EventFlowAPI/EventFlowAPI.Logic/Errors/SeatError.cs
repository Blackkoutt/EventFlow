using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.Response.Abstract;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record SeatError(HttpResponse? Details = null)
    {
        public static readonly Error SeatNotFound = new(new BadRequestResponse("Miejsce o podanym ID nie istnieje w bazie danych."));
        public static readonly Error SeatNotAvailable = new(new BadRequestResponse("Jedno z wybranych miejsc jest niedostępne, ponieważ ma rezerwację."));
        public static readonly Error SeatColumnOutOfRange = new(new BadRequestResponse("Numer kolumny miejsca jest większy niż liczba kolumn miejsc w sali."));
        public static readonly Error SeatGridColumnOutOfRange = new(new BadRequestResponse("Numer kolumny siatki miejsc jest większy niż maksymalna liczba kolumn miejsc w sali."));
        public static readonly Error SeatRowOutOfRange = new(new BadRequestResponse("Numer rzędu miejsca jest większy niż liczba rzędów miejsc w sali."));
        public static readonly Error SeatGridRowOutOfRange = new(new BadRequestResponse("Numer rzędu siatki miejsc jest większy niż maksymalna liczba rzędów miejsc w sali."));
        public static readonly Error NotAvailableSeatChanged = new(new BadRequestResponse("Pozycja lub typ niedostępnego miejsca zostały zmienione."));
        public static readonly Error SeatWithSuchRowAndColumnAlreadyExist = new(new BadRequestResponse("Miejsce o takim numerze rzędu i kolumny już istnieje."));
        public static readonly Error OtherSeatExistInSamePosition = new(new BadRequestResponse("Inne miejsce istnieje na tej samej pozycji w sali."));
        public static readonly Error SeatsDuplicate = new(new BadRequestResponse("Lista ID miejsc zawiera powtarzające się elementy."));
        public static readonly Error SeatsAreNotInGivenEventHall = new(new BadRequestResponse("Niektóre miejsca nie znajdują się w podanej sali wydarzenia."));
    }
}
