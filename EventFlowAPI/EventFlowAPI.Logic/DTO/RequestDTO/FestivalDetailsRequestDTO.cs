using EventFlowAPI.Logic.DTO.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.RequestDto
{
    public class FestivalDetailsRequestDto : IRequestDto
    {

        [MaxLength(2000, ErrorMessage = "Opis powinnien zawierać mniej niż 2000 znaków.")]
        public string? LongDescription { get; set; }
    }
}
