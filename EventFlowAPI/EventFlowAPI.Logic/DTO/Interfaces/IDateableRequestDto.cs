namespace EventFlowAPI.Logic.DTO.Interfaces
{
    public interface IDateableRequestDto
    {
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
    }
}
