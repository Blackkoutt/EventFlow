using EventFlowAPI.Logic.DTO.Abstract;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.Validators
{
    public sealed class MaxDurationValidator : ValidationAttribute
    {
        protected sealed override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (validationContext.ObjectInstance is StartEndDateAbstract datesObj)
            {
                TimeSpan duration = datesObj.EndDate - datesObj.StartDate;

                if (duration > datesObj.MaxDuration)
                {
                    return new ValidationResult(datesObj.MaxDurationErrorMessage);
                }
            }
            return ValidationResult.Success;
        }
    }
}
