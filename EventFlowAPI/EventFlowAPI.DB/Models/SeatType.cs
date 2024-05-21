using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Models
{
    public class SeatType
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string? Name { get; set; }

        [MaxLength(400)]
        public string? Description { get; set; }

        [Range(0.00, 99.99)]
        public double AddtionalPaymentPercentage { get; set; }

        public ICollection<Seat> Seats { get; set; } = new List<Seat>();
    }
}
