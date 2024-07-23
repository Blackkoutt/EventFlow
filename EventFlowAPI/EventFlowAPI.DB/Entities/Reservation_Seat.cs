namespace EventFlowAPI.DB.Entities
{
    public class Reservation_Seat
    {
        public int ReservationId { get; set; }  
        public int SeatId { get; set; }
        public Reservation? Reservation { get; set; }
        public Seat? Seat { get; set; }  
    }
}
