using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Models
{
    public class UserData
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string? Street { get; set; }

        [Range(0, 9999)]
        public int HouseNumber { get; set; }

        [Range(0, 9999)]
        public int FlatNumber { get; set; }

        [MaxLength(50)]
        public string? City { get; set; }

        [MaxLength(6)]
        public string? ZipCode { get; set; }

        [MaxLength(15)]
        public string? PhoneNumber { get; set; }

        public User? User { get; set; } 
    }
}
