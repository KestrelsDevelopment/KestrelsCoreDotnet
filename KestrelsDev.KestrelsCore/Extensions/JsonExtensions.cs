using System.Text.Json;
using KestrelsDev.KestrelsCore.ResultPattern;

namespace KestrelsDev.KestrelsCore.Extensions;

/// <summary>
/// Provides extension methods for working with JSON serialization, deserialization, and object cloning using JSON.
/// </summary>
public static class JsonExtensions
{
    /// <summary>
    /// Converts an object to its JSON string representation.
    /// </summary>
    /// <typeparam name="T">The type of the object to be serialized.</typeparam>
    /// <param name="obj">The object to serialize. Can be null.</param>
    /// <param name="options">Optional JSON serializer options for customizing serialization behavior.</param>
    /// <returns>A JSON string representation of the object.</returns>
    public static string ToJson<T>(this T? obj, JsonSerializerOptions? options = null)
    {
        return JsonSerializer.Serialize(obj, options);
    }

    /// <summary>
    /// Creates a deep clone of the given object by serializing it to JSON and then deserializing it back.
    /// </summary>
    /// <typeparam name="T">The type of the object to clone.</typeparam>
    /// <param name="obj">The object to be cloned.</param>
    /// <param name="options">Optional JSON serialization options to customize the process.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> containing the cloned object if successful,
    /// or an error if the deserialization process fails.
    /// </returns>
    public static Result<T> CloneJson<T>(this T obj, JsonSerializerOptions? options = null)
    {
        string serialized = obj.ToJson(options);

        T? cloned = JsonSerializer.Deserialize<T>(serialized, options);

        return cloned is not null ? cloned : (Error)"Failed to deserialize object";
    }
}