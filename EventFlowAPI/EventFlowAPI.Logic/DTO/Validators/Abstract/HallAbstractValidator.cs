using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.UnitOfWork;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.Validators.Abstract
{
    public abstract class HallAbstractValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (validationContext.ObjectInstance is HallRequestDto hallObj)
            {
                return ValidationRule(hallObj);
            }
            return ValidationResult.Success;
        }
        protected abstract ValidationResult? ValidationRule(HallRequestDto hallObj);
    }
}
