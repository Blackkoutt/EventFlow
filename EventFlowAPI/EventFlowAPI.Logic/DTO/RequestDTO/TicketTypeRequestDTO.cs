using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.RequestDTO
{
    public class TicketTypeRequestDTO
    {

        [Required(ErrorMessage = "Nazwa typu biletu jest wymagana.")]
        [Length(2, 40, ErrorMessage = "Nazwa powinna zawierać od 2 do 40 znaków.")]
        public string Name { get; set; } = string.Empty;
    }
}
