using EventFlowAPI.Logic.DTO.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.UpdateRequestDto
{
    public class UpdateEventPassTypeRequestDto : IRequestDto
    {
        [Required(ErrorMessage = "Cena karnetu jest wymagana.")]
        [DataType(DataType.Currency)]
        [Range(0, 9999.99, ErrorMessage = "Cena nie może być mniejsza niż 0 lub większa niż 9999.99.")]
        public decimal Price { get; set; }
    }
}
