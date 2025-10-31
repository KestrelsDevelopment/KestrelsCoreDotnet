namespace KestrelsDev.KestrelsCore.ResultPattern;

// ReSharper disable once NotAccessedPositionalProperty.Global
public record AggregateError(string Message, IReadOnlyList<Error> Errors, Exception? Exception = null)
    : Error(Message, Exception, Errors)
{
    public static implicit operator AggregateError(List<Error> errors)
        => new($"Multiple errors occurred, see {nameof(Errors)} for details.", errors);
}