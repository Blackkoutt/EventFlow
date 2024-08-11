using EventFlowAPI.Logic.DTO.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.RequestDto
{
    public class HallTypeRequestDto : IRequestDto, INameableRequestDto
    {

        [Required(ErrorMessage = "Nazwa typu sali jest wymagana.")]
        [Length(2, 30, ErrorMessage = "Nazwa powinna zawierać od 2 do 30 znaków.")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(600, ErrorMessage = "Opis powinien  zawierać mniej niż 600 znaków.")]
        public string? Description { get; set; }

        public List<int>? EquipmentIds { get; set; } = [];

    }
}
