using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.Response.Abstract;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record FestivalError(HttpResponse? Details = null)
    {
        public static readonly Error NotFound = new(new BadRequestResponse("Event with given Id does not exist in database."));
        public static readonly Error FestivalIsCanceled = new(new BadRequestResponse("Can not perform action because festival was canceled."));
        public static readonly Error FestivalIsExpired = new(new BadRequestResponse("Can not perform action because festival was expired."));
        public static readonly Error TooFewEventsInFestival = new(new BadRequestResponse("Festival must contains 2 or more events."));
        public static readonly Error TooMuchMediaPatronsInFestival = new(new BadRequestResponse("Festival can contains up to 10 media patrons."));
        public static readonly Error TooMuchOrganizersInFestival = new(new BadRequestResponse("Festival can contains up to 10 organizers."));
        public static readonly Error TooMuchSponsorsInFestival = new(new BadRequestResponse("Festival can contains up to 10 sponsors."));
        public static readonly Error TooMuchEventsInFestival = new(new BadRequestResponse("Festival can contains up to 12 events."));
        public static readonly Error FestivalIsTooLong = new(new BadRequestResponse("Festival can last up to 14 days."));

        public static readonly Error MediaPatronDuplicates = new(new BadRequestResponse("Media patron IDs list contains duplicated elements."));
        public static readonly Error EventDuplicates = new(new BadRequestResponse("Event IDs list contains duplicated elements."));
        public static readonly Error SponsorDuplicates = new(new BadRequestResponse("Sponsor IDs list contains duplicated elements."));
        public static readonly Error OrganizerDuplicates = new(new BadRequestResponse("Organizer IDs list contains duplicated elements."));
        public static readonly Error TooMuchTimeBetweenEvents = new(new BadRequestResponse("Maximum free time between events included in festival is 48 hours."));
    }
}
