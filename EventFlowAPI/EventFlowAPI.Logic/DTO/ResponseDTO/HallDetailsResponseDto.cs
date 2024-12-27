using EventFlowAPI.Logic.DTO.Abstract;

namespace EventFlowAPI.Logic.DTO.ResponseDto
{
    public class HallDetailsResponseDto : BaseResponseDto
    {
        public decimal TotalLength { get; set; }
        public decimal TotalWidth { get; set; }
        public decimal TotalArea { get; set; }
        public float? StageLength { get; set; }
        public float? StageWidth { get; set; }
        public decimal? StageArea { get; set; }
        public int NumberOfSeatsRows { get; set; }
        public int MaxNumberOfSeatsRows { get; set; }
        public int NumberOfSeatsColumns { get; set; }
        public int MaxNumberOfSeatsColumns { get; set; }
        public int NumberOfSeats { get; set; }
        public int MaxNumberOfSeats { get; set; }
    }
}
