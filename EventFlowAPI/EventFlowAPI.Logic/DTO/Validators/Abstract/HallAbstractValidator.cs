using EventFlowAPI.Logic.DTO.RequestDTO;
using EventFlowAPI.Logic.UnitOfWork;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.Validators.Abstract
{
    public abstract class HallAbstractValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var hallObj = validationContext.ObjectInstance as HallRequestDTO;
            if (hallObj != null)
            {
                return ValidationRule(hallObj);
            }
            return ValidationResult.Success;
        }
        protected abstract ValidationResult? ValidationRule(HallRequestDTO hallObj);
    }
}
