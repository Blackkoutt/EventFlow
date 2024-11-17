using EventFlowAPI.Logic.DTO.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.RequestDto
{
    public class EventCategoryRequestDto : IRequestDto, INameableRequestDto
    {

        [Required(ErrorMessage = "Nazwa kategorii wydarzenia jest wymagana.")]
        [Length(2, 30, ErrorMessage = "Nazwa powinna zawierać od 2 do 30 znaków.")]  
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nazwa ikony z Font Awensome jest wymagana.")]
        public string Icon { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kolor kategorii jest wymagany.")]
        [RegularExpression("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{8})$", ErrorMessage = "Kolor musi być w formacie HEX, np. #RRGGBB lub #RRGGBBAA.")]
        public string Color { get; set; } = string.Empty;
    }
}
