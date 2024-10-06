using EventFlowAPI.Logic.DTO.Abstract;

namespace EventFlowAPI.Logic.DTO.ResponseDto
{
    public class SeatTypeResponseDto : BaseResponseDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string SeatColor { get; set; } = string.Empty;
        public decimal AddtionalPaymentPercentage { get; set; }
    }
}
