using EventFlowAPI.Logic.DTO.Abstract;

namespace EventFlowAPI.Logic.DTO.ResponseDto
{
    public class HallResponseDto : BaseResponseDto
    {
        public int HallNr { get; set; }
        public decimal RentalPricePerHour { get; set; }
        public int Floor { get; set; }
        public int SeatsCount { get; set; } 
        public HallDetailsResponseDto? HallDetails { get; set; }
        public HallTypeResponseDto? Type { get; set; }
        public ICollection<SeatResponseDto> Seats { get; set; } = [];
    }
}
