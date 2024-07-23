using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventFlowAPI.DB.Models
{
    public class Hall
    {
        public int HallNr { get; set; }

        [Range(0.00, 999.99),
         Column(TypeName = "NUMERIC(5,2)")]
        public double RentalPricePerHour { get; set; }

        [Range(0,4),
         Column(TypeName = "NUMERIC(1)")]
        public int floor { get; set; }

        [Range(0.00, 999.99),
         Column(TypeName = "NUMERIC(5,2)")]
        public double Area { get; set; }

        [Range(0, 99),
         Column(TypeName = "NUMERIC(2)")]
        public int NumberOfSeatsRows { get; set; }

        [Range(0, 99),
         Column(TypeName = "NUMERIC(2)")]
        public int NumberOfSeatsColumns { get; set; }

        [Range(0, 999),
         Column(TypeName = "NUMERIC(3)")]
        public int MaxNumberOfSeats { get; set; }
        public int HallTypeId { get; set; }

        public HallType? Type { get; set; }

        public ICollection<Event> Events { get; set; } = [];
        public ICollection<Seat> Seats { get; set; } = [];
        public ICollection<HallRent> Rents { get; set; } = [];
    }
}
