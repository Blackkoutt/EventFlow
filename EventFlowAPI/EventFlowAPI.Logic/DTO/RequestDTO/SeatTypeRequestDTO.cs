using EventFlowAPI.Logic.DTO.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.RequestDto
{
    public class SeatTypeRequestDto : IRequestDto, INameableRequestDto
    {

        [Required(ErrorMessage = "Nazwa typu miejsca jest wymagana.")]
        [Length(2, 30, ErrorMessage = "Nazwa powinna zawierać od 2 do 30 znaków.")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(400, ErrorMessage = "Opis powinien zawierać mniej niż 400 znaków.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Kolor miejsca jest wymagany.")]
        [RegularExpression("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{8})$", ErrorMessage = "Kolor musi być w formacie HEX, np. #RRGGBB lub #RRGGBBAA.")]
        public string SeatColor { get; set; } = string.Empty;

        [Required(ErrorMessage = "Procent dodatkowej opłaty jest wymagany.")]
        [Range(0.00, 99.99, ErrorMessage = "Procent dodatkowej opłaty nie może być mniejszy niż 0 lub większy niż 99.99.")]
        public decimal AddtionalPaymentPercentage { get; set; }
    }
}
