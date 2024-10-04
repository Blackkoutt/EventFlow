﻿using System.Text.Json.Serialization;

namespace EventFlowAPI.Logic.Helpers.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum HallRentStatus
    {
        Active,
        Canceled,
        Expired
    }
}