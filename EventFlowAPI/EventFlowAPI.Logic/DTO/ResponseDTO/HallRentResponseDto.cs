using EventFlowAPI.Logic.DTO.Abstract;

namespace EventFlowAPI.Logic.DTO.ResponseDto
{
    public class HallRentResponseDto : BaseResponseDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime? CancelDate { get; set; }
        public decimal PaymentAmount { get; set; }     
        public PaymentTypeResponseDto? PaymentType { get; set; }
        public HallResponseDto? Hall { get; set; }  
        public UserResponseDto? User { get; set; }
        public ICollection<AdditionalServicesResponseDto> AdditionalServices { get; set; } = [];

    }
}
