using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.Validators.Abstract;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.Validators
{
    public class HallRowsValidator : HallAbstractValidator
    {
        protected override ValidationResult? ValidationRule(HallRequestDto hallObj)
        {
            if (hallObj.MaxNumberOfSeatsRows < hallObj.NumberOfSeatsRows)
            {
                return new ValidationResult("Podano niepoprawną ilość rzędów miejsc w sali.");
            }
            return ValidationResult.Success;
        }
    }
}
