namespace EventFlowAPI.Logic.DTO.Abstract
{
    public interface ICollisionalRequestDto
    {
        public int HallId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; } 
    }
}
