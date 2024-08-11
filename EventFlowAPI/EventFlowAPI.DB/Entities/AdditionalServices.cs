using EventFlowAPI.DB.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventFlowAPI.DB.Entities
{
    public class AdditionalServices : BaseEntity, INameableEntity
    {
        [MaxLength(40)]
        public string Name { get; set; } = string.Empty;

        [Range(0.00, 9999.99),
         Column(TypeName = "NUMERIC(6,2)")]
        public decimal Price {  get; set; }

        public ICollection<HallRent> Rents { get; set; } = [];
    }
}
