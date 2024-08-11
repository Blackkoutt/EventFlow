using EventFlowAPI.Logic.DTO.Interfaces;
using System.Text.Json.Serialization;

namespace EventFlowAPI.Logic.DTO.Abstract
{
    public abstract class BaseResponseDto : IResponseDto
    {
        [JsonPropertyOrder(-1)]
        public int Id { get; set; }
    }
}
