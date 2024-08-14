namespace EventFlowAPI.Logic.DTO.Interfaces
{
    public interface IDateableRequestDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
