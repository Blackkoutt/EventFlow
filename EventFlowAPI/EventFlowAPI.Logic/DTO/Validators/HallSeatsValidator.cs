using EventFlowAPI.Logic.DTO.RequestDTO;
using EventFlowAPI.Logic.DTO.Validators.Abstract;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.Validators
{
    public class HallSeatsValidator : HallAbstractValidator
    {
        protected override ValidationResult? ValidationRule(HallRequestDTO hallObj)
        {
            if (hallObj.MaxNumberOfSeats < hallObj.NumberOfSeats)
            {
                return new ValidationResult("Liczba miejsc w sali nie może przekraczać ustalonej maksymalnej liczby miejsc.");
            }
            return ValidationResult.Success;
        }
    }
}
