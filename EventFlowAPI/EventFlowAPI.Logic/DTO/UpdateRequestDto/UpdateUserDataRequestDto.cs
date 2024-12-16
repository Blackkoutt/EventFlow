using EventFlowAPI.Logic.DTO.Interfaces;
using EventFlowAPI.Logic.DTO.Validators;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.UpdateRequestDto
{
    public class UpdateUserDataRequestDto : IRequestDto
    {
        [Required(ErrorMessage = "Nazwa ulicy jest wymagana.")]
        [Length(2, 50, ErrorMessage = "Nazwa ulicy powinna zawierać od 2 do 50 znaków.")]
        [RegularExpression(@"^[A-ZĄĆĘŁŃÓŚŹŻ0-9][A-Za-ząćęłńóśźż0-9 ]*$", ErrorMessage = "Nazwa ulicy powinna zawierać tylko litery lub cyfry oraz powinna znaczynać się od dużej litery lub cyfry.")]
        public string Street { get; set; } = string.Empty;

        [Required(ErrorMessage = "Numer domu jest wymagany.")]
        [Range(1, 999, ErrorMessage = "Numer domu nie może być mniejszy niż 1 lub większa niż 999.")]
        public int HouseNumber { get; set; }

        [Range(1, 999, ErrorMessage = "Numer mieszkania nie może być mniejszy niż 1 lub większa niż 999.")]
        public int? FlatNumber { get; set; }

        [Required(ErrorMessage = "Nazwa miasta jest wymagana.")]
        [Length(2, 50, ErrorMessage = "Nazwa miasta powinna zawierać od 2 do 50 znaków.")]
        [RegularExpression(@"^[A-ZĄĆĘŁŃÓŚŹŻ][a-ząćęłńóśźż ]*$", ErrorMessage = "Nazwa miasta powinna zawierać tylko litery i zaczynać się wielką literą.")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kod pocztowy jest wymagany.")]
        [Length(6, 6, ErrorMessage = "Kod pocztowy powinien zawierać 6 znaków.")]
        [DataType(DataType.PostalCode)]
        [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Format kodu pocztowego to 'xx-xxx', gdzie x to liczby od 0 do 9.")]
        public string ZipCode { get; set; } = string.Empty;

        // custom validator
        [Length(5, 15, ErrorMessage = "Numer telefonu powinien zawierać od 5 do 15 znaków.")]
        [Required(ErrorMessage = "Numer telefonu jest wymagany.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\+?\d{1,4}[\s-]?)?(\(?\d{1,5}\)?[\s-]?)?[\d\s-]{5,15}$", ErrorMessage = "Numer telefonu jest nieprawidłowy.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [MaxFileSizeValidator(10)]
        public IFormFile? UserPhoto { get; set; }
    }
}
