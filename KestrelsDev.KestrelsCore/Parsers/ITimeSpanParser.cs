using KestrelsDev.KestrelsCore.ResultPattern;

namespace KestrelsDev.KestrelsCore.Parsers;

/// <summary>
/// Defines the contract for parsing string representations of time spans into TimeSpan objects.
/// </summary>
public interface ITimeSpanParser
{
    /// <summary>
    /// A default implementation of the <see cref="ITimeSpanParser"/> interface,
    /// used as the standard parsing mechanism for converting strings into
    /// TimeSpan instances.
    /// </summary>
    /// <remarks>
    /// This static property provides a pre-configured instance of the
    /// <see cref="DefaultTimeSpanParser"/> class, ensuring consistent
    /// parsing behavior across the application.
    /// </remarks>
    public static readonly ITimeSpanParser DefaultImpl = new DefaultTimeSpanParser();

    /// Tries to parse the provided string into a TimeSpan value.
    /// <param name="s">The string to parse into a TimeSpan. Can be null or empty.</param>
    /// <param name="value">The resulting TimeSpan value if the parsing was successful. Defaults to TimeSpan.Zero if parsing fails.</param>
    /// <returns>True if the string was successfully parsed into a TimeSpan; otherwise, false.</returns>
    public bool TryParse(string? s, out TimeSpan value);

    /// <summary>
    /// Parses a string representation of a time interval into a TimeSpan object, using structured error handling.
    /// </summary>
    /// <param name="s">The input string to parse as a TimeSpan. This parameter can be null.</param>
    /// <returns>
    /// A Result object containing the parsed TimeSpan if the string is successfully parsed.
    /// If parsing fails, the Result will contain an error message.
    /// </returns>
    public Result<TimeSpan> Parse(string? s);
}