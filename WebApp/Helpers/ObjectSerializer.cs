using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApp.Helpers;

public static class ObjectSerializer
{
    public static JsonSerializerOptions GetOptions { get; } =
        new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            WriteIndented = true,
        };
}
