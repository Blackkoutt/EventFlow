using EventFlowAPI.Logic.DTO.Validators;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.RequestDTO
{
    public class UserRequestDTO
    {

        [Required(ErrorMessage = "Imię jest wymagane.")]
        [Length(2, 40, ErrorMessage = "Imię powinno zawierać od 2 do 40 znaków.")]
        [RegularExpression(@"^[A-ZĄĆĘŁŃÓŚŹŻ][a-ząćęłńóśźż]*$", ErrorMessage = "Imię powinno zawierać tylko litery i zaczynać się wielką literą.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nazwisko jest wymagane.")]
        [Length(2, 40, ErrorMessage = "Nazwisko powinno zawierać od 2 do 40 znaków.")]
        [RegularExpression(@"^[A-ZĄĆĘŁŃÓŚŹŻ][a-ząćęłńóśźż]*$", ErrorMessage = "Nazwisko powinno zawierać tylko litery i zaczynać się wielką literą.")]
        public string Surname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Adres e-mail jest wymagany.")]
        [Length(5, 255, ErrorMessage="Adres e-mail powinien zawierać od 5 do 255 znaków.")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Adres e-mail ma niepoprawny format.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Data urodzenia jest wymagana.")]
        [DataType(DataType.Date)]
        [DateOfBirthValidator(13)]
        public DateTime DateOfBirth { get; set; }
    }
}
