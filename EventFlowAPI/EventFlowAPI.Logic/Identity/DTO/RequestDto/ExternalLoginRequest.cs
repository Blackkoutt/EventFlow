using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.Identity.DTO.RequestDto
{
    public class ExternalLoginRequest
    {
        public string Code { get; set; } = string.Empty;
    }
}
