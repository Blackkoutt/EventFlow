using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EventFlowAPI.DB.Models
{
    public class Organizer
    {
        public int Id { get; set; }

        [NotNull]
        [MaxLength(40)]
        public string? Name { get; set; }

        public ICollection<Festival_Organizer> Festivals { get; set; } = [];
    }
}
