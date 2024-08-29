using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.Identity.DTO.ResponseDto
{
    public class UserRegisterResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
