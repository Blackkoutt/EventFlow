using System.Text.Json.Serialization;

namespace EventFlowAPI.Logic.Helpers.PayU
{
    public class PayURequestPaymentDto
    {
        [JsonPropertyName("customerIp")]
        public string CustomerIp { get; set; } = "127.0.0.1";

        [JsonPropertyName("merchantPosId")]
        public string MerchantPosId { get; set; } = "486905";

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("continueUrl")]
        public string ContinueUrl { get; set; } = string.Empty;

        [JsonPropertyName("currencyCode")]
        public string CurrencyCode { get; set; } = "PLN";

        [JsonPropertyName("totalAmount")]
        public int TotalAmount { get; set; }

        [JsonPropertyName("products")]
        public List<PayUProductDto> Products { get; set; } = [];

        [JsonPropertyName("buyer")]
        public PayUBuyerDto Buyer { get; set; } = default!;
    }
}
