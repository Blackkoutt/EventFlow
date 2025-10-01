using System.Text.Json.Serialization;

namespace EventFlowAPI.Logic.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Status
    {
        Active,
        Canceled,
        Expired,
        Unknown
    }
}
