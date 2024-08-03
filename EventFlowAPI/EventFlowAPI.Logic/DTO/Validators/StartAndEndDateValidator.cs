using EventFlowAPI.Logic.DTO.Abstract;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.Validators
{
    public class StartAndEndDateValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var datesObj = validationContext.ObjectInstance as StartEndDateAbstract;
            if (datesObj != null)
            {
                if (datesObj.StartDate < DateTime.Now)
                {
                    return new ValidationResult("Data początkowa nie może być wcześniejsza niż obcena data.");
                }
                if (datesObj.EndDate < DateTime.Now)
                {
                    return new ValidationResult("Data końcowa nie może być wcześniejsza niż obcena data.");
                }
                if ((datesObj.EndDate - datesObj.StartDate).TotalMinutes < 5)
                {
                    return new ValidationResult("Data zakończenia musi być co najmniej 5 minut po dacie początkowej.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
