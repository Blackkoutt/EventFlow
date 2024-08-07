using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.Validators.Abstract;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.Validators
{
    public class HallMaxSeatsValidator : HallAbstractValidator
    {
        protected override ValidationResult? ValidationRule(HallRequestDto hallObj)
        {
            if (hallObj.MaxNumberOfSeatsColumns * hallObj.MaxNumberOfSeatsRows != hallObj.MaxNumberOfSeats)
            {
                return new ValidationResult("Podano niepoprawną maksymalną ilość miejsc.");
            }
            return ValidationResult.Success;
        }
    }
}
