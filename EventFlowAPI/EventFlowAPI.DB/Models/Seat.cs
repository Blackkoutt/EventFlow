using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Models
{
    public class Seat
    {
        public int Id { get; set; }

        [Range(0, 999)]
        public int SeatNr { get; set; }

        [Range(0, 99)]
        public int Row { get; set; }

        [Range(0, 99)]
        public int Column { get; set; }

        public int SeatTypeId { get; set; }
        public int HallNr { get; set; } 

        public SeatType? SeatType { get; set; }
        public Hall? Hall { get; set; }
        public ICollection<Reservation_Seat> Reservations { get; set; } = new List<Reservation_Seat>();
    }
}
