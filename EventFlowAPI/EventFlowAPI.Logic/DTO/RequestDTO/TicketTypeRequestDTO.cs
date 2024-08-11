using EventFlowAPI.Logic.DTO.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.RequestDto
{
    public class TicketTypeRequestDto : IRequestDto, INameableRequestDto
    {

        [Required(ErrorMessage = "Nazwa typu biletu jest wymagana.")]
        [Length(2, 40, ErrorMessage = "Nazwa powinna zawierać od 2 do 40 znaków.")]
        public string Name { get; set; } = string.Empty;
    }
}
