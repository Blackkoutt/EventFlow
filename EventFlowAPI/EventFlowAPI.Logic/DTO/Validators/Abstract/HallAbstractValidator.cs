using EventFlowAPI.Logic.DTO.RequestDto;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.Validators.Abstract
{
    public abstract class HallAbstractValidator : ValidationAttribute
    {
        protected sealed override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (validationContext.ObjectInstance is HallDetailsRequestDto hallObj)
            {
                return ValidationRule(hallObj);
            }
            return ValidationResult.Success;
        }
        protected abstract ValidationResult? ValidationRule(HallDetailsRequestDto hallObj);
    }
}
