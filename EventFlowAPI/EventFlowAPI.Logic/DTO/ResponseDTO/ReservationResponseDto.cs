using EventFlowAPI.Logic.DTO.Abstract;

namespace EventFlowAPI.Logic.DTO.ResponseDto
{
    public class ReservationResponseDto : BaseResponseDto
    {
        public DateTime ReservationDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public UserResponseDto? User { get; set; }
        public PaymentTypeResponseDto? PaymentType { get; set; }
        public TicketResponseDto? Ticket { get; set; }
        public ICollection<SeatResponseDto> Seats { get; set; } = [];
    }
}
