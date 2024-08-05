using EventFlowAPI.DB.Entities.Abstract;
using EventFlowAPI.Logic.DTO.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventFlowAPI.Logic.DTO.ResponseDto
{
    public class EventTicketResponseDto : BaseResponseDto
    {
        public decimal Price { get; set; }
        public EventResponseDto? Event { get; set; }
        public TicketTypeResponseDto? TicketType { get; set; }
    }
}
