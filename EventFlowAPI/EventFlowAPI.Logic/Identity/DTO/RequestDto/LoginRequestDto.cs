using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.Identity.DTO.RequestDto
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "Adres e-mail jest wymagany.")]
        [Length(5, 255, ErrorMessage = "Adres e-mail powinien zawierać od 5 do 255 znaków.")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Adres e-mail ma niepoprawny format.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Hasło jest wymagane.")]
        public string Password { get; set; } = string.Empty;
    }
}
