using EventFlowAPI.Logic.DTO.Abstract;
using EventFlowAPI.Logic.DTO.RequestDto;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.Validators
{
    public class MaxFileSizeValidator : ValidationAttribute
    {
        private readonly int _maxFileSize;

        public MaxFileSizeValidator(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected sealed override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (validationContext.ObjectInstance is IFormFile file)
            {
                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult($"Rozmiar pliku nie może być większy niż {_maxFileSize} MB.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
