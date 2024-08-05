using EventFlowAPI.Logic.DTO.Abstract;

namespace EventFlowAPI.Logic.DTO.ResponseDto
{
    public class AdditionalServicesResponseDto : BaseResponseDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price {  get; set; }
    }
}
