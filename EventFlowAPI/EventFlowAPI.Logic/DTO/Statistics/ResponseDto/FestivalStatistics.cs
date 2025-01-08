namespace EventFlowAPI.Logic.DTO.Statistics.ResponseDto
{
    public class FestivalStatistics
    {
        public int AllAddedFestivalsCount { get; set; }
        public int AllFestivalsThatTookPlaceInTimePeriod { get; set; }
        public int AllCanceledFestivalsCount { get; set; }
        public int DurationAvg { get; set; }
        public int EventCountAvg { get; set; }
        public double TotalFestivalsIncome { get; set; }
        public Dictionary<string, double> MostPopularFestivals { get; set; } = [];
        public Dictionary<string, double> MostProfitableFestivals { get; set; } = [];
        public Dictionary<string, double> OrganizatorFestivalsDict { get; set; } = [];
        public Dictionary<string, double> MediaPatronFestivalsDict { get; set; } = [];
        public Dictionary<string, double> SponsorFestivalsDict { get; set; } = [];

    }
}
