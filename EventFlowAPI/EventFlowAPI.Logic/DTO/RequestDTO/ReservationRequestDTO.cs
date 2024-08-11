using EventFlowAPI.Logic.DTO.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.RequestDto
{
    public class ReservationRequestDto : IRequestDto
    {
        [Required(ErrorMessage = "Należy wybrać typ płatności.")]
        public int PaymentTypeId { get; set; }

        [Required(ErrorMessage = "Należy wybrać bilet.")]
        public int EventTicketId { get; set; }

        [Required(ErrorMessage = "Wybierz co najmniej jedno miejsce.")]
        public List<int> SeatsIds { get; set; } = [];
    }
}
