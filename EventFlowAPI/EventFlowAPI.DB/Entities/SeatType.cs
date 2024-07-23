using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace EventFlowAPI.DB.Models
{
    public class SeatType
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(400)]
        public string? Description { get; set; }

        [Range(0.00, 99.99),
         Column(TypeName = "NUMERIC(4,2)")]
        public double AddtionalPaymentPercentage { get; set; }

        public ICollection<Seat> Seats { get; set; } = [];
    }
}
