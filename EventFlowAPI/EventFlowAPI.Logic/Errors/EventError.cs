using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.Response.Abstract;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record EventError(HttpResponse? Details = null)
    {
        public static readonly Error CategoryNotFound = new(new BadRequestResponse("Kategoria wydarzenia o podanym ID nie istnieje w bazie danych."));
        public static readonly Error EventNotFound = new(new BadRequestResponse("Wydarzenie o podanym ID nie istnieje w bazie danych."));
        public static readonly Error HallNotFound = new(new BadRequestResponse("Sala wydarzenia o podanym ID nie istnieje w bazie danych."));
        public static readonly Error CollisionWithExistingEvent = new(new BadRequestResponse("Wydarzenie koliduje z innym istniejącym wydarzeniem. Zmień salę lub daty rozpoczęcia/kończenia wydarzenia."));
        public static readonly Error CollisionWithExistingHallRent = new(new BadRequestResponse("Wydarzenie koliduje z istniejącym wynajmem sali. Zmień salę lub daty rozpoczęcia/kończenia wydarzenia."));
        public static readonly Error NotFound = new(new BadRequestResponse("Wydarzenie o podanym ID nie istnieje w bazie danych."));
        public static readonly Error EventIsNotInSuchHall = new(new BadRequestResponse("Wydarzenie o podanym ID nie odbywa się w sali o podanym ID."));
        public static readonly Error EventIsDeleted = new(new BadRequestResponse("Nie można wykonać operacji, ponieważ wydarzenie zostało anulowane."));
        public static readonly Error EventIsExpired = new(new BadRequestResponse("Nie można wykonać operacji, ponieważ wydarzenie wygasło."));
        public static readonly Error EventCanNotHaveManySameTicketTypes = new(new BadRequestResponse("Wydarzenie może mieć tylko jeden bilet danego typu."));
        public static readonly Error EventIsPartOfTooMuchFestivals = new(new BadRequestResponse("Wydarzenie może być częścią maksymalnie 3 festiwali."));
    }
}

