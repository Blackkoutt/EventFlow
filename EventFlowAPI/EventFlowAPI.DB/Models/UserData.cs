using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace EventFlowAPI.DB.Models
{
    public class UserData
    {
        public int Id { get; set; }

        [NotNull]
        [MaxLength(50)]
        public string? Street { get; set; }

        [Range(0, 9999),
         Column(TypeName = "NUMERIC(4)")]
        public int HouseNumber { get; set; }

        [Range(0, 9999),
         Column(TypeName = "NUMERIC(4)")]
        public int FlatNumber { get; set; }

        [NotNull]
        [MaxLength(50)]
        public string? City { get; set; }

        [NotNull]
        [MaxLength(6)]
        public string? ZipCode { get; set; }

        [NotNull]
        [MaxLength(15)]
        public string? PhoneNumber { get; set; }

        public User? User { get; set; } 
    }
}
