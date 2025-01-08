namespace EventFlowAPI.Logic.DTO.Statistics.ResponseDto
{
    public class EventStatistics
    {
        public int AllAddedEventsCount { get; set; }
        public int AllEventsThatTookPlaceInTimePeriod { get; set; }
        public int AllCanceledEventsCount { get; set; }
        public int DurationAvg { get; set; }
        public double TotalEventsIncome { get; set; }
        public Dictionary<string, double>  MostPopularEvents { get; set; } = [];
        public Dictionary<string, double>  MostProfitableEvents { get; set; } = [];
        public Dictionary<string, double> EventHallDict { get; set; } = [];
        public Dictionary<string, double> EventCategoryDict { get; set; } = [];
    }
}
