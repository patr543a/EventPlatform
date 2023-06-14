using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace EventPlatform.Entities.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Status
{
    [EnumMember(Value = "Server couldn't give a status")]
    None = 0,

    [EnumMember(Value = "Success")]
    Success,

    [EnumMember(Value = "Access was denied")]
    AccessDenied,

    [EnumMember(Value = "Body was invalid")]
    InvalidObject,

    [EnumMember(Value = "Resource not found")]
    NotFound,

    [EnumMember(Value = "Failed due to data already existing")]
    AlreadyPresent,
}
