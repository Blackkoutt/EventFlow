using EventFlowAPI.Logic.DTO.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.UpdateRequestDto
{
    public class UpdateEquipmentRequestDto : IRequestDto
    {
        [MaxLength(200, ErrorMessage = "Opis powinnien zawierać mniej niż 200 znaków.")]
        public string? Description { get; set; }
    }
}
