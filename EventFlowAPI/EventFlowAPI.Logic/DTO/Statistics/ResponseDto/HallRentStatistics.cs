using EventFlowAPI.DB.Entities;

namespace EventFlowAPI.Logic.DTO.Statistics.ResponseDto
{
    public class HallRentStatistics
    {
        public int AllAddedHallRentsCount { get; set; }
        public int AllHallRentsThatTookPlaceInTimePeriod { get; set; }
        public int AllCanceledHallRentsCount { get; set; }
        public TimeSpan DurationAvg { get; set; }
        public double TotalHallRentsIncome { get; set; }
        public Dictionary<string, double> UserReservationsDict { get; set; } = [];
        public Dictionary<string, double> HallReservationsDict { get; set; } = [];
        public Dictionary<string, double> HallTypeReservationsDict { get; set; } = [];
        public Dictionary<string, double> HallAddtionalServicesReservationsDict { get; set; } = [];
    }
}
