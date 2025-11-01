using System.Text.Json;
using KestrelsDev.KestrelsCore.ResultPattern;

namespace KestrelsDev.KestrelsCore.Extensions;

public static class JsonExtensions
{
    public static string ToJson<T>(this T? obj, JsonSerializerOptions? options = null)
    {
        return JsonSerializer.Serialize(obj, options);
    }

    public static Result<T> CloneJson<T>(this T obj, JsonSerializerOptions? options = null)
    {
        string serialized = obj.ToJson(options);

        T? cloned = JsonSerializer.Deserialize<T>(serialized, options);

        return cloned is not null ? cloned : (Error)"Failed to deserialize object";
    }
}