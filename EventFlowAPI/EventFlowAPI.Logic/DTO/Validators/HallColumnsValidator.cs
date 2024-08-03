using EventFlowAPI.Logic.DTO.RequestDTO;
using EventFlowAPI.Logic.DTO.Validators.Abstract;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.Validators
{
    public class HallColumnsValidator : HallAbstractValidator
    {
        protected override ValidationResult? ValidationRule(HallRequestDTO hallObj)
        {
            if (hallObj.MaxNumberOfSeatsColumns < hallObj.NumberOfSeatsColumns)
            {
                return new ValidationResult("Podano niepoprawną ilość kolumn miejsc w sali.");
            }
            return ValidationResult.Success;
        }
    }
}
