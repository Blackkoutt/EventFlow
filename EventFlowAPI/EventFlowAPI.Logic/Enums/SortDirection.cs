using System.Text.Json.Serialization;

namespace EventFlowAPI.Logic.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SortDirection
    {
        ASC,
        DESC
    }
}
