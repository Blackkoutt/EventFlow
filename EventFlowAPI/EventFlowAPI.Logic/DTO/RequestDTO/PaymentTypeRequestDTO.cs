using EventFlowAPI.Logic.DTO.Interfaces;
using EventFlowAPI.Logic.DTO.Validators;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.RequestDto
{
    public class PaymentTypeRequestDto : IRequestDto, INameableRequestDto
    {

        [Required(ErrorMessage = "Nazwa typu płatności jest wymagana.")]
        [Length(2, 30, ErrorMessage = "Nazwa powinna zawierać od 2 do 30 znaków.")]
        public string Name { get; set; } = string.Empty;

        [MaxFileSizeValidator(10)]
        public IFormFile? PaymentTypePhoto { get; set; }
    }
}
