using EventFlowAPI.Logic.DTO.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.UpdateRequestDto
{
    public class UpdateFAQRequestDto : IRequestDto, IQuestionableRequestDto
    {
        [Required(ErrorMessage = "Treść pytania jest wymagana.")]
        [Length(2, 100, ErrorMessage = "Pytanie powinno zawierać od 2 do 100 znaków.")]
        public string Question { get; set; } = string.Empty;

        [Required(ErrorMessage = "Treść odpowiedzi jest wymagana.")]
        [Length(2, 1000, ErrorMessage = "Odpowiedź powinna zawierać od 2 do 1000 znaków.")]
        public string Answer { get; set; } = string.Empty;
    }
}
