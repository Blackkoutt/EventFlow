namespace EventFlowAPI.Logic.DTO.Statistics.ResponseDto
{
    public class ReservationStatistics
    {
        public int AllNewReservationsCount { get; set; }
        public int AllCanceledReservationsCount { get; set; }
        public int AllNewEventReservationsCount { get; set; }
        public int AllNewFestivalReservationsCount { get; set; }
        public Dictionary<string, double> ReservationFestivalEventsDict { get; set; } = [];
        public Dictionary<string, double> ReservationTicketsTypesDict { get; set; } = [];
        public Dictionary<string, double> ReservationSeatTypesDict { get; set; } = [];

    }
}
