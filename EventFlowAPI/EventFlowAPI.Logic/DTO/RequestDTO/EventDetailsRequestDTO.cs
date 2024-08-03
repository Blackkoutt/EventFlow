using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.RequestDTO
{
    public class EventDetailsRequestDTO
    { 

        [MaxLength(2000, ErrorMessage = "Opis powinnien zawierać mniej niż 2000 znaków.")]
        public string? LongDescription { get; set; }
    }
}
