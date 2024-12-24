using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Text.Json.Serialization;

namespace EventFlowAPI.Logic.Helpers.PayU
{
    public class PayUProductDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("unitPrice")]
        public int Price { get; set; }

        [JsonPropertyName("quantity")]
        public int Quanitity { get; set; }
    }
}
