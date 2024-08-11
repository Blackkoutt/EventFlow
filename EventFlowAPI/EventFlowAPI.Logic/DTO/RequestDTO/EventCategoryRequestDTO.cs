using EventFlowAPI.Logic.DTO.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.RequestDto
{
    public class EventCategoryRequestDto : IRequestDto, INameableRequestDto
    {

        [Required(ErrorMessage = "Nazwa kategorii wydarzenia jest wymagana.")]
        [Length(2, 30, ErrorMessage = "Nazwa powinna zawierać od 2 do 30 znaków.")]  
        public string Name { get; set; } = string.Empty;
    }
}
