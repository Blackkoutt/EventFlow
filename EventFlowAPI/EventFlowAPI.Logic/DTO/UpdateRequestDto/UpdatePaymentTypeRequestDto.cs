using EventFlowAPI.Logic.DTO.Interfaces;
using EventFlowAPI.Logic.DTO.Validators;
using Microsoft.AspNetCore.Http;

namespace EventFlowAPI.Logic.DTO.UpdateRequestDto
{
    public class UpdatePaymentTypeRequestDto : IRequestDto
    {
        [MaxFileSizeValidator(10)]
        public IFormFile? PaymentTypePhoto { get; set; }
    }
}
