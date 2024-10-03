namespace EventFlowAPI.Logic.DTO.Abstract
{
    public interface IHallDetailsRequestDto
    {
        public decimal? StageArea { get; set; }
        public int NumberOfSeatsRows { get; set; }
        public int MaxNumberOfSeatsRows { get; set; }
        public int NumberOfSeatsColumns { get; set; }
        public int MaxNumberOfSeatsColumns { get; set; }
    }
}
