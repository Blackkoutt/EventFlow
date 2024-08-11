using EventFlowAPI.Logic.DTO.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.RequestDto
{
    public class OrganizerRequestDto : IRequestDto, INameableRequestDto
    {
        [Required(ErrorMessage = "Nazwa organizatora jest wymagana.")]
        [Length(2, 50, ErrorMessage = "Nazwa powinna zawierać od 2 do 50 znaków.")]
        public string Name { get; set; } = string.Empty;
    }
}
