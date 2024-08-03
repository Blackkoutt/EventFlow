using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.RequestDTO
{
    public class EventPassTypeRequestDTO
    {
        [Required(ErrorMessage = "Nazwa typu karnetu jest wymagana.")]
        [Length(2, 40, ErrorMessage = "Nazwa powinna zawierać od 2 do 40 znaków.")]     
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Długość")]
        [Range(1, 60, ErrorMessage = "Okres ważności karnetu powinien wynosić od 1 do 60 miesięcy.")]
        public int ValidityPeriodInMonths { get; set; }

        [Required(ErrorMessage = "Cena karnetu jest wymagana.")]
        [DataType(DataType.Currency)]
        [Range(0, 9999.99, ErrorMessage = "Cena nie może być mniejsza niż 0 lub większa niż 9999.99.")]     
        public decimal Price { get; set; }
    }
}
