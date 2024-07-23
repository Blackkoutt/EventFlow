using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventFlowAPI.DB.Entities
{
    public class UserData
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Street { get; set; } = string.Empty;

        [Range(0, 9999),
         Column(TypeName = "NUMERIC(4)")]
        public int HouseNumber { get; set; }

        [Range(0, 9999),
         Column(TypeName = "NUMERIC(4)")]
        public int FlatNumber { get; set; }

        [MaxLength(50)]
        public string City { get; set; } = string.Empty;

        [MaxLength(6)]
        public string ZipCode { get; set; } = string.Empty;

        [MaxLength(15)]
        public string PhoneNumber { get; set; } = string.Empty;

        public User? User { get; set; } 
    }
}
