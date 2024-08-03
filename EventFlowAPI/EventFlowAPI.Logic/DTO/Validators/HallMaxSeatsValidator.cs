using EventFlowAPI.Logic.DTO.RequestDTO;
using EventFlowAPI.Logic.DTO.Validators.Abstract;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.Validators
{
    public class HallMaxSeatsValidator : HallAbstractValidator
    {
        protected override ValidationResult? ValidationRule(HallRequestDTO hallObj)
        {
            if (hallObj.MaxNumberOfSeatsColumns * hallObj.MaxNumberOfSeatsRows != hallObj.MaxNumberOfSeats)
            {
                return new ValidationResult("Podano niepoprawną maksymalną ilość miejsc.");
            }
            return ValidationResult.Success;
        }
    }
}
