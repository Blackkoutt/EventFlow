using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EventFlowAPI.DB.Models
{
    public class Sponsor
    {
        public int Id { get; set; }

        [MaxLength(25)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Festival_Sponsor> Festivals { get; set; } = [];
    }
}
