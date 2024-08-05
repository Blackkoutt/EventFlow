using EventFlowAPI.Logic.DTO.Abstract;

namespace EventFlowAPI.Logic.DTO.ResponseDto
{
    public class EventDetailsResponseDto : BaseResponseDto
    {
        public string? LongDescription { get; set; }
        //public Event? Event { get; set; }
    }
}
