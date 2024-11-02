using EventFlowAPI.Logic.DTO.Abstract;

namespace EventFlowAPI.Logic.DTO.ResponseDto
{
    public class HallTypeResponseDto : BaseResponseDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string PhotoName { get; set; } = string.Empty;
        public string PhotoEndpoint { get; set; } = string.Empty;
        public ICollection<EquipmentResponseDto> Equipments { get; set; } = [];
    }
}
