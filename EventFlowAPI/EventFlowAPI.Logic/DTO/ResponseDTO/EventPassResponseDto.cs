using EventFlowAPI.Logic.DTO.Abstract;

namespace EventFlowAPI.Logic.DTO.ResponseDto
{
    public class EventPassResponseDto : BaseResponseDto
    {
        public DateTime StartDate { get; set; }
        public DateTime? RenewalDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? CancelDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public EventPassTypeResponseDto? PassType { get; set; }
        public UserResponseDto? User { get; set; }
        public PaymentTypeResponseDto? PaymentType { get; set; }
    }
}
