using EventFlowAPI.Logic.DTO.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.UpdateRequestDto
{
    public class UpdateEventPassRequestDto : IRequestDto
    {
        [Required(ErrorMessage = "Należy podać typ karnetu.")]
        public int PassTypeId { get; set; }
    }
}
