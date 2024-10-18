using EventFlowAPI.Logic.DTO.Abstract;

namespace EventFlowAPI.Logic.DTO.ResponseDto
{
    public class TicketResponseDto : BaseResponseDto
    {
        public decimal Price { get; set; }
        public EventResponseDto? Event { get; set; } = default!;
        public FestivalResponseDto? Festival { get; set; } = default!;
        public TicketTypeResponseDto TicketType { get; set; } = default!;
    }
}
