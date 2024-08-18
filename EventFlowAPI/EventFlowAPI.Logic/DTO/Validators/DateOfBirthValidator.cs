using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.Validators
{
    public class DateOfBirthValidator : ValidationAttribute
    {
        private readonly int minBirthDate;
        public DateOfBirthValidator(int minBirthDate) 
        {
            this.minBirthDate = minBirthDate;
        }
        protected sealed override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime dateOfBirth)
            {
                if (dateOfBirth >= DateTime.Now.AddYears(-minBirthDate))
                {
                    return new ValidationResult($"Aby posiadać konto musisz mieć skończone conajmniej {minBirthDate} lat.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
