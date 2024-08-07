﻿using EventFlowAPI.Logic.DTO.RequestDTO;
using EventFlowAPI.Logic.DTO.Validators.Abstract;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.Validators
{
    public class HallAreaValidator : HallAbstractValidator
    {
        protected override ValidationResult? ValidationRule(HallRequestDTO hallObj)
        {
            if(hallObj.StageArea != null)
            {
                if (hallObj.TotalLength * hallObj.TotalWidth != hallObj.TotalArea)
                {
                    return new ValidationResult("Podano niepoprawną powierzchnię sali.");
                }
                if (hallObj.TotalArea - 100 < hallObj.StageArea)
                {
                    return new ValidationResult("Powierzchnia sceny jest zbyt duża względem rozmiaru sali.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
