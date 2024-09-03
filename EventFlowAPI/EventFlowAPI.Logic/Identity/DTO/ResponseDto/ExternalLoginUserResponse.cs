using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace EventFlowAPI.Logic.Identity.DTO.ResponseDto
{
    public class ExternalLoginUserResponse
    {
        public string? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;   
    }
}
