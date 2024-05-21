using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Models
{
    public class Hall
    {
        [Key]
        public int HallNr { get; set; }

        [Range(0.00, 999.99)]
        public double RentalPricePerHour { get; set; }

        [Range(0,4)]
        public int floor { get; set; }

        [Range(0.00, 999.99)]
        public double Area { get; set; }

        [Range(0, 99)]
        public int NumberOfSeatsRows { get; set; }

        [Range(0, 99)]
        public int NumberOfSeatsColumns { get; set; }

        [Range(0, 999)]
        public int MaxNumberOfSeats { get; set; }
        public int HallTypeId { get; set; }

        public HallType? Type { get; set; }

        public ICollection<Event> Events { get; set; } = new List<Event>();
        public ICollection<Seat> Seats { get; set; } = new List<Seat>();
        public ICollection<HallRent> Rents { get; set; } = new List<HallRent>();
    }
}
