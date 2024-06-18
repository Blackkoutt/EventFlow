using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EventFlowAPI.DB.Models
{
    public class Sponsor
    {
        public int Id { get; set; }

        [NotNull]
        [MaxLength(25)]
        public string? Name { get; set; }

        public ICollection<Festival_Sponsor> Festivals { get; set; } = [];
    }
}
