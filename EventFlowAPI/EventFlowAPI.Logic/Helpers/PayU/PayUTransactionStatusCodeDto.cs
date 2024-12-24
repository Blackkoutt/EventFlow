using System.Text.Json.Serialization;

namespace EventFlowAPI.Logic.Helpers.PayU
{
    public class PayUTransactionStatusCodeDto
    {
        [JsonPropertyName("statusCode")]
        public string StatusCode { get; set; } = string.Empty;
    }
}
