using KestrelsDev.KestrelsCore.Extensions;

namespace KestrelsDev.KestrelsCore.ResultPattern;

/// <summary>
/// Represents an error with a descriptive message, an optional exception providing additional context,
/// and an optional payload containing supplementary information.
/// Includes functionality for implicit conversions between strings, exceptions, and lists of errors.
/// </summary>
/// <param name="Message">The descriptive message of the error.</param>
/// <param name="Exception">The associated exception that caused the error, if any.</param>
/// <param name="Payload">Additional contextual information or data related to the error, if provided.</param>
public record Error(string Message, Exception? Exception = null, object? Payload = null)
{
    /// <summary>
    /// Represents an error with an associated message, optional exception, and optional payload.
    /// Provides additional functionality for implicit conversions and methods for error comparison.
    /// </summary>
    /// <param name="Message">The error message describing what went wrong.</param>
    /// <param name="Exception">Optional exception that caused the error.</param>
    /// <param name="Payload">Optional additional data or context related to the error.</param>
    public Error(string Message, object Payload) : this(Message, null, Payload)
    {
    }

    /// <summary>
    /// Provides implicit operator overloads for conversions to and from the <see cref="Error"/> type.
    /// </summary>
    /// <remarks>
    /// Enables implicit conversions for common types like <see cref="string"/>, <see cref="Exception"/>, and
    /// collections of errors, allowing streamlined handling of error representations.
    /// </remarks>
    /// <param name="message">
    /// Implicitly converts a <see cref="string"/> to an <see cref="Error"/> with the given message.
    /// </param>
    public static implicit operator Error(string message) => new(message);

    /// <summary>
    /// Defines an implicit conversion operator for creating an error instance from an exception.
    /// </summary>
    /// <param name="ex">The exception to be converted into an error.</param>
    /// <returns>An error instance with the associated message and exception details.</returns>
    public static implicit operator Error(Exception ex) => new(ex.Message, ex);

    /// <summary>
    /// Defines an implicit conversion from an <see cref="Error"/> object to its string representation,
    /// specifically the value of the <see cref="Error.Message"/> property.
    /// </summary>
    /// <param name="error">The <see cref="Error"/> instance to be converted.</param>
    /// <returns>The <see cref="Error.Message"/> as a string representation of the error.</returns>
    public static implicit operator string(Error error) => error.Message;

    /// <summary>
    /// Defines a custom implicit conversion for the <see cref="Error"/> type.
    /// Allows seamless conversion between <see cref="Error"/> instances and other data types or entities.
    /// </summary>
    /// <param name="errors">A collection of <see cref="Error"/> instances to aggregate into a single error object.</param>
    /// <returns>
    /// An <see cref="Error"/> object representing the aggregate of all provided errors.
    /// Typically used to consolidate multiple errors into a unified representation.
    /// </returns>
    public static implicit operator Error(List<Error> errors)
        => new AggregateError($"Multiple errors occurred, see {nameof(AggregateError.Errors)} for details.", errors);

    /// <summary>
    /// Returns a string that represents the current error.
    /// </summary>
    /// <returns>The error message contained within the Error instance.</returns>
    public override string ToString() => Message;

    /// <summary>
    /// Determines whether the current error is similar to another error by comparing their messages, ignoring case.
    /// </summary>
    /// <param name="other">The error to compare against the current error.</param>
    /// <returns>True if the messages of both errors are equal when compared case-insensitively; otherwise, false.</returns>
    public bool IsSimilarTo(Error other) => Message.EqualsIgnoreCase(other.Message);
}