using EventFlowAPI.DB.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventFlowAPI.DB.Entities
{
    public class AdditionalServices : BaseEntity, INameableEntity, IPriceableEntity, ISoftDeleteable, IUpdateableEntity, ISoftUpdateable
    {
        [MaxLength(40)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Description { get; set; }

        [Range(0.00, 9999.99),
         Column(TypeName = "NUMERIC(6,2)")]
        public decimal Price {  get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeleteDate { get; set; }
        public bool IsSoftUpdated { get; set; } = false;
        public bool IsUpdated { get; set; } = false;
        public DateTime? UpdateDate { get; set; }
        public ICollection<HallRent> Rents { get; set; } = [];
    }
}
