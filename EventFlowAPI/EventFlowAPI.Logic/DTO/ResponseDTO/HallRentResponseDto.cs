using EventFlowAPI.Logic.DTO.Abstract;
using EventFlowAPI.Logic.Enums;

namespace EventFlowAPI.Logic.DTO.ResponseDto
{
    public class HallRentResponseDto : BaseResponseDto
    {
        public Guid HallRentGuid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status HallRentStatus { get; set; }
        public decimal PaymentAmount { get; set; }     
        public PaymentTypeResponseDto? PaymentType { get; set; }
        public HallResponseDto? Hall { get; set; }  
        public UserResponseDto? User { get; set; }
        public ICollection<AdditionalServicesResponseDto> AdditionalServices { get; set; } = [];

    }
}
