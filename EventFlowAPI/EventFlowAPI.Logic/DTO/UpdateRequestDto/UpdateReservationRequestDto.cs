using EventFlowAPI.Logic.DTO.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.UpdateRequestDto
{
    public class UpdateReservationRequestDto : IRequestDto
    {
        [Required(ErrorMessage = "Należy wybrać typ płatności.")]
        public int PaymentTypeId { get; set; }

        [Required(ErrorMessage = "Należy wybrać bilet.")]
        public int TicketId { get; set; }
        public bool IsReservationForFestival { get; set; } = false;

        [Required(ErrorMessage = "Wybierz co najmniej jedno miejsce.")]
        public List<int> SeatsIds { get; set; } = [];
    }
}
