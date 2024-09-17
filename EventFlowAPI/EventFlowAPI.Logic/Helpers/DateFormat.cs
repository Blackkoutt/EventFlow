namespace EventFlowAPI.Logic.Helpers
{
    public sealed record DateFormat
    {
        public static readonly string Date = "dd.MM.yyyy";
        public static readonly string DateTime = "dd.MM.yyyy HH:mm";
        public static readonly string DateTimeFullMonth = "dd MMMM yyyy HH:mm";
        public static readonly string DateTimeFullDayAndMonth = "dddd, dd MMMM yyyy, GODZ. HH:mm";
    }
}
