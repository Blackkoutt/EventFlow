using EventFlowAPI.Logic.DTO.Abstract;

namespace EventFlowAPI.Logic.DTO.ResponseDto
{
    public class EventPassTypeResponseDto : BaseResponseDto
    {
        public string Name { get; set; } = string.Empty;
        public int ValidityPeriodInMonths { get; set; }
        public decimal Price { get; set; }
    }
}
