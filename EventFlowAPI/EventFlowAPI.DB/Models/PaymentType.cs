using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Models
{
    public class PaymentType
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string? Name { get; set; }
        public ICollection<HallRent> HallRents { get; set; } = new List<HallRent>();
        public ICollection<EventPass> EventPasses { get; set; } = new List<EventPass>();
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
