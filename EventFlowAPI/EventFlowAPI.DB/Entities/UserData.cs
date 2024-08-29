using EventFlowAPI.DB.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventFlowAPI.DB.Entities
{
    public class UserData : IEntity
    {
        public string Id { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Street { get; set; } = string.Empty;

        [Range(0, 999),
         Column(TypeName = "NUMERIC(3)")]
        public int HouseNumber { get; set; }

        [Range(0, 999),
         Column(TypeName = "NUMERIC(3)")]
        public int? FlatNumber { get; set; }

        [MaxLength(50)]
        public string City { get; set; } = string.Empty;

        [MaxLength(6)]
        public string ZipCode { get; set; } = string.Empty;

        [MaxLength(15)]
        public string PhoneNumber { get; set; } = string.Empty;

        public User? User { get; set; } 
    }
}
