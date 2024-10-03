using EventFlowAPI.Logic.DTO.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.RequestDto
{
    public class HallRent_HallRequestDto : IRequestDto
    {

        [Required(ErrorMessage = "Szczegóły dotyczące sali są wymagane.")]
        public HallRent_HallDetailsRequestDto HallDetails { get; set; } = default!;

        [Required(ErrorMessage = "Typ sali jest wymagany.")]
        public int HallTypeId { get; set; }

        public ICollection<SeatRequestDto> Seats { get; set; } = [];
    }
}
