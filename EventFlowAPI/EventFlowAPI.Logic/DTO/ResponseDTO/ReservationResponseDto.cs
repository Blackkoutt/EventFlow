using EventFlowAPI.Logic.DTO.Abstract;
using EventFlowAPI.Logic.Enums;
using System.Text.Json.Serialization;

namespace EventFlowAPI.Logic.DTO.ResponseDto
{
    public class ReservationResponseDto : BaseResponseDto
    {
        public Guid ReservationGuid { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status ReservationStatus { get; set; }
        public decimal PaymentAmount { get; set; }
        public UserResponseDto? User { get; set; }
        public PaymentTypeResponseDto? PaymentType { get; set; }
        public TicketResponseDto? Ticket { get; set; }
        public ICollection<SeatResponseDto> Seats { get; set; } = [];
    }
}
