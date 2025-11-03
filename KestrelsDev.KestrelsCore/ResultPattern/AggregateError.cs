namespace KestrelsDev.KestrelsCore.ResultPattern;

// ReSharper disable once NotAccessedPositionalProperty.Global
/// <summary>
/// Represents an aggregate error consisting of multiple individual errors.
/// Inherits from the <see cref="Error"/> class to unify error reporting and
/// enable operations that involve collections of errors.
/// </summary>
/// <param name="Message">The descriptive message summarizing the aggregate error.</param>
/// <param name="Errors">A read-only collection of individual errors aggregated into this error.</param>
/// <param name="Exception">An optional exception providing additional context for the aggregate error.</param>
public record AggregateError(string Message, IReadOnlyList<Error> Errors, Exception? Exception = null)
    : Error(Message, Exception, Errors)
{
    /// <summary>
    /// Defines an implicit conversion operator to create an instance of <see cref="AggregateError"/>
    /// from a list of <see cref="Error"/> instances.
    /// </summary>
    /// <param name="errors">The list of individual errors to aggregate.</param>
    /// <returns>An <see cref="AggregateError"/> containing a message and the provided list of errors.</returns>
    public static implicit operator AggregateError(List<Error> errors)
        => new($"Multiple errors occurred, see {nameof(Errors)} for details.", errors);
}