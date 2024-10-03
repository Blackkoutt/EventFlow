using EventFlowAPI.Logic.DTO.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.RequestDto
{
    public class Event_FestivalTicketRequestDto : IRequestDto
    {
        [Required(ErrorMessage = "Cena biletu na wydarzenie jest wymagana.")]
        [Range(0, 999.99, ErrorMessage = "Cena nie może być mniejsza niż 0 lub większa niż 999.99.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Należy podać typ biletu.")]
        public int TicketTypeId { get; set; }
    }
}
