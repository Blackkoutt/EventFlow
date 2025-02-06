using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.Response.Abstract;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record FestivalError(HttpResponse? Details = null)
    {
        public static readonly Error NotFound = new(new BadRequestResponse("Wydarzenie o podanym ID nie istnieje w bazie danych."));
        public static readonly Error FestivalIsDeleted = new(new BadRequestResponse("Nie można wykonać operacji, ponieważ festiwal został anulowany."));
        public static readonly Error FestivalIsExpired = new(new BadRequestResponse("Nie można wykonać operacji, ponieważ festiwal wygasł."));
        public static readonly Error TooFewEventsInFestival = new(new BadRequestResponse("Festiwal musi zawierać co najmniej 2 wydarzenia."));
        public static readonly Error TooMuchMediaPatronsInFestival = new(new BadRequestResponse("Festiwal może zawierać maksymalnie 10 patronów medialnych."));
        public static readonly Error TooMuchOrganizersInFestival = new(new BadRequestResponse("Festiwal może zawierać maksymalnie 10 organizatorów."));
        public static readonly Error TooMuchSponsorsInFestival = new(new BadRequestResponse("Festiwal może zawierać maksymalnie 10 sponsorów."));
        public static readonly Error TooMuchEventsInFestival = new(new BadRequestResponse("Festiwal może zawierać maksymalnie 12 wydarzeń."));
        public static readonly Error FestivalIsTooLong = new(new BadRequestResponse("Festiwal może trwać maksymalnie 14 dni."));

        public static readonly Error MediaPatronDuplicates = new(new BadRequestResponse("Lista ID patronów medialnych zawiera duplikaty."));
        public static readonly Error EventDuplicates = new(new BadRequestResponse("Lista ID wydarzeń zawiera duplikaty."));
        public static readonly Error SponsorDuplicates = new(new BadRequestResponse("Lista ID sponsorów zawiera duplikaty."));
        public static readonly Error OrganizerDuplicates = new(new BadRequestResponse("Lista ID organizatorów zawiera duplikaty."));
        public static readonly Error TooMuchTimeBetweenEvents = new(new BadRequestResponse("Maksymalny czas wolny pomiędzy wydarzeniami w festiwalu wynosi 48 godzin."));
    }
}
