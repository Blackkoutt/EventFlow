using EventFlowAPI.Logic.DTO.Abstract;

namespace EventFlowAPI.Logic.DTO.ResponseDto
{
    public class HallResponseDto : BaseResponseDto
    {
        public int HallNr { get; set; }
        public decimal RentalPricePerHour { get; set; }
        public int Floor { get; set; }
        public decimal TotalLength { get; set; }
        public decimal TotalWidth { get; set; }
        public decimal TotalArea { get; set; }
        public decimal? StageArea { get; set; }
        public int NumberOfSeatsRows { get; set; }
        public int MaxNumberOfSeatsRows { get; set; }
        public int NumberOfSeatsColumns { get; set; }
        public int MaxNumberOfSeatsColumns { get; set; }
        public int NumberOfSeats { get; set; }
        public int MaxNumberOfSeats { get; set; }
        public HallTypeResponseDto? Type { get; set; }
        public ICollection<SeatResponseDto> Seats { get; set; } = [];
    }
}
