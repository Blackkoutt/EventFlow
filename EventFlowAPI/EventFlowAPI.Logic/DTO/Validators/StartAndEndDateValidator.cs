using EventFlowAPI.Logic.DTO.Abstract;
using EventFlowAPI.Logic.DTO.RequestDto;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.Validators
{
    public sealed class StartAndEndDateValidator : ValidationAttribute
    {
        protected sealed override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (validationContext.ObjectInstance is StartEndDateAbstract datesObj)
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

            if(validationContext.ObjectInstance is FestivalUpdate_EventRequestDto requestDto)
            {
                if (requestDto.StartDate != null && requestDto.StartDate < DateTime.Now)
                {
                    return new ValidationResult("Data początkowa nie może być wcześniejsza niż obcena data.");
                }
                if (requestDto.EndDate != null && requestDto.EndDate < DateTime.Now)
                {
                    return new ValidationResult("Data końcowa nie może być wcześniejsza niż obcena data.");
                }
                if (requestDto.StartDate != null &&
                    requestDto.EndDate != null &&
                    ((TimeSpan)(requestDto.EndDate - requestDto.StartDate)).TotalMinutes < 5)
                {
                    return new ValidationResult("Data zakończenia musi być co najmniej 5 minut po dacie początkowej.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
