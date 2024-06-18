using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace EventFlowAPI.DB.Models
{
    public class EventPassType
    {
        public int Id { get; set; }

        [NotNull]
        [MaxLength(40)]
        public string? Name { get; set; }

        [Range(0.00, 9999.99),
         Column(TypeName = "NUMERIC(6,2)")]
        public double Price { get; set; }

        public ICollection<EventPass> EventPasses { get; set; } = new List<EventPass>();
    }
}
