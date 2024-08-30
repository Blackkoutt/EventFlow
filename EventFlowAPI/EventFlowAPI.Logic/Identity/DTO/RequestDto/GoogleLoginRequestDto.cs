using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.Identity.DTO.RequestDto
{
    public class GoogleLoginRequestDto
    {
        public string Code { get; set; } = string.Empty;
    }
}
