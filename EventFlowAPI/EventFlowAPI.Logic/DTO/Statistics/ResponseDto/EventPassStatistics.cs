namespace EventFlowAPI.Logic.DTO.Statistics.ResponseDto
{
    public class EventPassStatistics
    {
        public int AllBougthEventPassesCount { get; set; }
        public int AllRenewedEventPassesCount { get; set; }
        public int AllCanceledEventPassesCount { get; set; }
        public double TotalEventPassesIncome { get; set; }
        public Dictionary<string, double> EventPassTypeDict { get; set; } = [];
        public Dictionary<string, double> MostProfitableEventsPassTypeDict { get; set; } = [];
    }
}
