using EventFlowAPI.Logic.DTO.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.RequestDto
{
    public class EventPassRequestDto : IRequestDto
    {
        [Required(ErrorMessage = "Należy podać typ karnetu.")]
        public int PassTypeId { get; set; }

        [Required(ErrorMessage = "Należy podać typ płatności.")]
        public int PaymentTypeId { get; set; } 
    }
}
