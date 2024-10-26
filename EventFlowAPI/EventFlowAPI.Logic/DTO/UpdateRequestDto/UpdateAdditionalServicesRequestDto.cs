using EventFlowAPI.Logic.DTO.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.UpdateRequestDto
{
    public class UpdateAdditionalServicesRequestDto : IRequestDto
    {
        [Required(ErrorMessage = "Cena usługi jest wymagana.")]
        [DataType(DataType.Currency)]
        [Range(0, 9999.99, ErrorMessage = "Cena nie może być mniejsza niż 0 lub większa niż 9999.99.")]
        public decimal Price { get; set; }

        [MaxLength(200, ErrorMessage = "Opis powinnien zawierać mniej niż 200 znaków.")]
        public string? Description { get; set; }
    }
}
