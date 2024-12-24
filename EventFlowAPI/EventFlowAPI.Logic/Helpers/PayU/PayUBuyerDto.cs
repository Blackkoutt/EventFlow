using System.Text.Json.Serialization;

namespace EventFlowAPI.Logic.Helpers.PayU
{
    public class PayUBuyerDto
    {

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; } = string.Empty;

        [JsonPropertyName("lastName")]
        public string LastName { get; set; } = string.Empty;

        [JsonPropertyName("language")]
        public string Language { get; set; } = "pl";
    }
}
