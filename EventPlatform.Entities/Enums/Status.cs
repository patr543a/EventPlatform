using System.Text.Json.Serialization;

namespace EventPlatform.Entities.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Status
{
    None = 0,
    Success,
    AccessDenied,
    InvalidObject,
    NotFound,
}
