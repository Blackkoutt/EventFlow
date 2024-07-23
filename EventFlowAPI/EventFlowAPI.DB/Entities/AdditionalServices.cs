using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventFlowAPI.DB.Entities
{
    public class AdditionalServices
    {
        public int Id { get; set; }

        [MaxLength(40)]
        public string Name { get; set; } = string.Empty;

        [Range(0.00, 9999.99),
         Column(TypeName = "NUMERIC(6,2)")]
        public double Price {  get; set; }

        public ICollection<HallRent_AdditionalServices> Rents { get; set; } = [];
    }
}
