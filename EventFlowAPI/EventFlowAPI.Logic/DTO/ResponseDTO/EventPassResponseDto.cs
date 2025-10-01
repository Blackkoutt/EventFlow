using EventFlowAPI.Logic.DTO.Abstract;
using EventFlowAPI.Logic.Enums;

namespace EventFlowAPI.Logic.DTO.ResponseDto
{
    public class EventPassResponseDto : BaseResponseDto
    {
        public Guid EventPassGuid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? RenewalDate { get; set; }
        public DateTime EndDate { get; set; }
        public Status EventPassStatus { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public EventPassTypeResponseDto? PassType { get; set; }
        public UserResponseDto? User { get; set; }
        public PaymentTypeResponseDto? PaymentType { get; set; }
    }
}
