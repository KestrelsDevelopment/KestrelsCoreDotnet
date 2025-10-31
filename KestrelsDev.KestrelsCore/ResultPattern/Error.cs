using KestrelsDev.KestrelsCore.Extensions;

namespace KestrelsDev.KestrelsCore.ResultPattern;

/// <summary>
/// Error type that wraps an error message and optional a causing exception and/or a payload.
/// </summary>
/// <param name="Message">The error message.</param>
/// <param name="Exception">The exception that caused the error.</param>
/// <param name="Payload">Additional data about the error. Can be used in logging to provide additional context.</param>
public record Error(string Message, Exception? Exception = null, object? Payload = null)
{
    public Error(string Message, object Payload) : this(Message, null, Payload)
    {
    }

    public static implicit operator Error(string message) => new(message);
    public static implicit operator Error(Exception ex) => new(ex.Message, ex);

    public static implicit operator string(Error error) => error.Message;

    public static implicit operator Error(List<Error> errors)
        => new AggregateError($"Multiple errors occurred, see {nameof(AggregateError.Errors)} for details.", errors);

    public override string ToString() => Message;

    public bool IsSimilarTo(Error other) => Message.EqualsIgnoreCase(other.Message);
}