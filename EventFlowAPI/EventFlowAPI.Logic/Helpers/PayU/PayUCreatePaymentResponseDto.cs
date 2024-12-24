using System.Text.Json.Serialization;

namespace EventFlowAPI.Logic.Helpers.PayU
{
    public class PayUCreatePaymentResponseDto
    {
        [JsonPropertyName("status")]
        public PayUTransactionStatusCodeDto Status { get; set; } = default!;

        [JsonPropertyName("redirectUri")]
        public string RedirectUri { get; set; } = string.Empty;

        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
    }
}
