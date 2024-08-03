using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.RequestDTO
{
    public class EquipmentRequestDTO
    {

        [Required(ErrorMessage = "Nazwa wyposażenia jest wymagana.")]
        [Length(2, 40, ErrorMessage = "Nazwa powinna zawierać od 2 do 40 znaków.")]     
        public string Name { get; set; } = string.Empty;

        [MaxLength(200, ErrorMessage = "Opis powinnien zawierać mniej niż 200 znaków.")]
        public string? Description { get; set; }
    }
}
