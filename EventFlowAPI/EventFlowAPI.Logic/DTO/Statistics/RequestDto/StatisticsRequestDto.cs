namespace EventFlowAPI.Logic.DTO.Statistics.RequestDto
{
    public class StatisticsRequestDto
    {
        public DateTime StartDate { get; set; } = DateTime.MinValue;
        public DateTime EndDate { get; set; } = DateTime.MaxValue;
        public bool IncludeStatisticsAboutHallRent { get; set; } = false;
        public bool IncludeStatisticsAboutEvent { get; set; } = false;
        public bool IncludeStatisticsAboutEventPasses { get; set; } = false;
        public bool IncludeStatisticsAboutFestivals { get; set; } = false;
        public bool IncludeStatisticsAboutReservations { get; set; } = false;
        public bool IncludeStatisticsAboutPayments { get; set; } = false;
        public bool IncludeStatisticsAboutUsers { get; set; } = false;
    }
}
