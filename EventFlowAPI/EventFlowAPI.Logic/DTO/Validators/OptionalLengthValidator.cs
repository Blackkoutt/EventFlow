using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.Validators
{
    public class OptionalLengthValidator : ValidationAttribute
    {
        private readonly int _minLength;
        private readonly int _maxLength;

        public OptionalLengthValidator(int minLength, int maxLength)
        {
            _minLength = minLength;
            _maxLength = maxLength;
        }

        public sealed override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return true;

            var strValue = value.ToString();
            return strValue!.Length >= _minLength && strValue.Length <= _maxLength;
        }
    }
}
