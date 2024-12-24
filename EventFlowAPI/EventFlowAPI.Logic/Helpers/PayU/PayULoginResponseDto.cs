using System.Text.Json.Serialization;

namespace EventFlowAPI.Logic.Helpers.PayU
{
    public class PayULoginResponseDto
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; } = string.Empty;

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; } = string.Empty;

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonPropertyName("grant_type")]
        public string GrantType { get; set; } = string.Empty;
    }
}
