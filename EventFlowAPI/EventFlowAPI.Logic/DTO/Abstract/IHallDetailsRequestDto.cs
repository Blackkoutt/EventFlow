namespace EventFlowAPI.Logic.DTO.Abstract
{
    public interface IHallDetailsRequestDto
    {
        public float? StageLength { get; set; }
        public float? StageWidth { get; set; }
       // public int NumberOfSeatsRows { get; set; }
        public int MaxNumberOfSeatsRows { get; set; }
       // public int NumberOfSeatsColumns { get; set; }
        public int MaxNumberOfSeatsColumns { get; set; }
    }
}
