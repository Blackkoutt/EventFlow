using System.Text.Json.Serialization;

namespace EventFlowAPI.Logic.Helpers.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Status
    {
        Active,
        Canceled,
        Expired
    }
}
