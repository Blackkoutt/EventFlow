using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventFlowAPI.DB.Entities
{
    public class Reservation_Seat
    {
        public int ReservationId { get; set; }
        public int SeatId { get; set; }

        [ForeignKey("SeatId")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual Seat? Seat { get; set; }

        [ForeignKey("ReservationId")]
        [DeleteBehavior(DeleteBehavior.NoAction)] 
        public virtual Reservation? Reservation { get; set; }
    }
}
