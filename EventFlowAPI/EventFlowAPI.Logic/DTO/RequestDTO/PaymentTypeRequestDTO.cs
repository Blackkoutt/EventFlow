using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.RequestDTO
{
    public class PaymentTypeRequestDTO
    {

        [Required(ErrorMessage = "Nazwa typu płatności jest wymagana.")]
        [Length(2, 30, ErrorMessage = "Nazwa powinna zawierać od 2 do 30 znaków.")]
        public string Name { get; set; } = string.Empty;
    }
}
