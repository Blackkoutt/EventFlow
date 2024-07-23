using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventFlowAPI.DB.Entities
{
    public class Seat
    {
        public int Id { get; set; }

        [Range(0, 999),
         Column(TypeName = "NUMERIC(3)")]
        public int SeatNr { get; set; }

        [Range(0, 99),
         Column(TypeName = "NUMERIC(2)")]
        public int Row { get; set; }

        [Range(0, 99),
         Column(TypeName = "NUMERIC(2)")]
        public int Column { get; set; }

        public int SeatTypeId { get; set; }
        public int HallNr { get; set; } 

        public SeatType? SeatType { get; set; }
        public Hall? Hall { get; set; }
        public ICollection<Reservation_Seat> Reservations { get; set; } = [];
    }
}
