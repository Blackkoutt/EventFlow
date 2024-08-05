using EventFlowAPI.Logic.DTO.Interfaces;

namespace EventFlowAPI.Logic.DTO.Abstract
{
    public abstract class BaseResponseDto : IResponseDto
    {
        public int Id { get; set; }
    }
}
