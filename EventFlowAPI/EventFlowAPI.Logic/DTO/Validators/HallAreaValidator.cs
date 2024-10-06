using EventFlowAPI.Logic.DTO.RequestDto
    ;
using EventFlowAPI.Logic.DTO.Validators.Abstract;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.Validators
{
    public class HallAreaValidator : HallAbstractValidator
    {
        protected sealed override ValidationResult? ValidationRule(HallDetailsRequestDto hallObj)
        {
            if (hallObj.StageLength != null && hallObj.StageWidth == null)
                return new ValidationResult("Należy podać szerokość sceny.");

            if (hallObj.StageWidth != null && hallObj.StageLength == null)
                return new ValidationResult("Należy podać długość sceny.");

            if (hallObj.StageWidth != null && hallObj.StageLength != null)
            {
                if (hallObj.TotalLength * hallObj.TotalWidth - 100 < (decimal)(hallObj.StageWidth * hallObj.StageLength))
                {
                    return new ValidationResult("Powierzchnia sceny jest zbyt duża względem rozmiaru sali.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
