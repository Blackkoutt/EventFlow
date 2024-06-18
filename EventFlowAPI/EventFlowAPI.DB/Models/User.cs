using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EventFlowAPI.DB.Models
{
    public class User
    {
        public int Id { get; set; }

        [NotNull]
        [MaxLength(30)]
        public string? Name { get; set; }

        [NotNull]
        [MaxLength(40)]
        public string? Surname { get; set; }

        [NotNull]
        [MaxLength(255)]
        public string? Email { get; set; }

        public DateTime DateOfBirth { get; set; }
        public UserData? UserData { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        public ICollection<EventPass> EventPasses { get; set; } = new List<EventPass>();
        public ICollection<HallRent> HallRents { get; set; } = new List<HallRent>();
    }
}
