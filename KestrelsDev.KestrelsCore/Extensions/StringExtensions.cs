using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using KestrelsDev.KestrelsCore.Parsers;
using KestrelsDev.KestrelsCore.ResultPattern;

namespace KestrelsDev.KestrelsCore.Extensions;

/// <summary>
/// Provides a collection of extension methods for the <see cref="string"/> class, offering additional functionality
/// for case-insensitive comparisons, validation for null or whitespace, string manipulation using regular expressions,
/// and parsing of various data types such as integers, doubles, booleans, time spans, and dates.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Compares two strings for equality, ignoring case.
    /// </summary>
    /// <param name="str">The first string to compare.</param>
    /// <param name="other">The second string to compare.</param>
    /// <returns>True if the two strings are equal, ignoring case; otherwise, false.</returns>
    public static bool EqualsIgnoreCase(this string str, string other)
        => str.Equals(other, StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// Determines whether a specified string is null or an empty string.
    /// </summary>
    /// <param name="str">The string to check for null or emptiness.</param>
    /// <returns>True if the string is null or empty; otherwise, false.</returns>
    public static bool IsNullOrEmpty([NotNullWhen(false)] this string? str) => string.IsNullOrEmpty(str);

    /// <summary>
    /// Determines whether the specified string is null, empty, or consists only of white-space characters.
    /// </summary>
    /// <param name="str">The string to evaluate.</param>
    /// <returns>True if the string is null, empty, or contains only white-space characters; otherwise, false.</returns>
    public static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? str) => string.IsNullOrWhiteSpace(str);

    /// <summary>
    /// Determines whether the specified string is empty.
    /// </summary>
    /// <param name="str">The string to check.</param>
    /// <returns>True if the string is empty; otherwise, false.</returns>
    public static bool IsEmpty(this string str) => str.Length == 0;

    /// <summary>
    /// Determines whether the specified string is empty or consists solely of whitespace characters.
    /// </summary>
    /// <param name="str">The input string to evaluate.</param>
    /// <returns>
    /// True if the string is empty or consists only of whitespace characters; otherwise, false.
    /// </returns>
    public static bool IsEmptyOrWhiteSpace(this string str) => str.Length == 0 || str.Trim().Length == 0;

    /// <summary>
    /// Determines whether the specified input string matches the provided regular expression pattern.
    /// </summary>
    /// <param name="str">The input string to test against the regular expression pattern.</param>
    /// <param name="pattern">The regular expression pattern to be matched.</param>
    /// <param name="options">Options that modify the matching behavior, such as case-insensitivity or culture-invariant matching. The default value is RegexOptions.Compiled.</param>
    /// <param name="timeout">A time interval specifying the maximum time allowed for the match operation. If not provided, the method uses Regex.InfiniteMatchTimeout.</param>
    /// <returns>
    /// true if the input string matches the regular expression pattern; otherwise, false.
    /// </returns>
    public static bool IsMatch(
        this string str,
        [StringSyntax("Regex", "options")] string pattern,
        RegexOptions options = RegexOptions.Compiled,
        TimeSpan? timeout = null)
        => Regex.IsMatch(str, pattern, options, timeout ?? Regex.InfiniteMatchTimeout);

    /// <summary>
    /// Finds all occurrences of the specified regular expression pattern in the input string.
    /// </summary>
    /// <param name="str">The input string to search for matches.</param>
    /// <param name="pattern">The regular expression pattern to match. This must conform to .NET regular expression syntax.</param>
    /// <param name="options">A bitwise combination of enumeration values that specify options for matching. The default is RegexOptions.Compiled.</param>
    /// <param name="timeout">The time-out interval for the match operation. If not specified, it defaults to infinite timeout.</param>
    /// <returns>A MatchCollection containing all the matches found in the input string. If no matches are found, an empty MatchCollection is returned.</returns>
    public static MatchCollection Matches(
        this string str,
        [StringSyntax("Regex", "options")] string pattern,
        RegexOptions options = RegexOptions.Compiled,
        TimeSpan? timeout = null)
        => Regex.Matches(str, pattern, options, timeout ?? Regex.InfiniteMatchTimeout);

    /// <summary>
    /// Searches the input string for the first occurrence of the regular expression pattern
    /// and returns a <see cref="System.Text.RegularExpressions.Match"/> object
    /// containing details about the match.
    /// </summary>
    /// <param name="str">The input string to search within.</param>
    /// <param name="pattern">The regular expression pattern to find in the input string.</param>
    /// <param name="options">
    /// The regular expression options that modify the matching behavior, such as case-insensitivity.
    /// The default value is <see cref="System.Text.RegularExpressions.RegexOptions.Compiled"/>.
    /// </param>
    /// <param name="timeout">
    /// The maximum duration allowed for the match operation. If not specified, the default value
    /// is <see cref="System.Text.RegularExpressions.Regex.InfiniteMatchTimeout"/>.
    /// </param>
    /// <returns>
    /// A <see cref="System.Text.RegularExpressions.Match"/> object that contains information
    /// about the first match found in the string, or an empty match object if no match is found.
    /// </returns>
    public static Match Match(
        this string str,
        [StringSyntax("Regex", "options")] string pattern,
        RegexOptions options = RegexOptions.Compiled,
        TimeSpan? timeout = null)
        => Regex.Match(str, pattern, options, timeout ?? Regex.InfiniteMatchTimeout);

    /// <summary>
    /// Replaces occurrences of matches defined by a regular expression pattern in the string with a string computed by a MatchEvaluator.
    /// </summary>
    /// <param name="str">The original string to process for replacements.</param>
    /// <param name="pattern">The regular expression pattern to identify matches.</param>
    /// <param name="evaluator">
    /// A delegate that processes each match and defines the replacement string based on the match.
    /// </param>
    /// <param name="options">
    /// Specifies options to modify the behavior of the pattern matching. Defaults to RegexOptions.Compiled.
    /// </param>
    /// <param name="timeout">
    /// The maximum amount of time allowed for the matching operation. If not specified, Regex.InfiniteMatchTimeout is used.
    /// </param>
    /// <returns>A string with matches replaced by the values computed by the specified MatchEvaluator.</returns>
    public static string ReplaceMatches(
        this string str,
        [StringSyntax("Regex", "options")] string pattern,
        MatchEvaluator evaluator,
        RegexOptions options = RegexOptions.Compiled,
        TimeSpan? timeout = null)
        => Regex.Replace(str, pattern, evaluator, options, timeout ?? Regex.InfiniteMatchTimeout);

    /// <summary>
    /// Attempts to parse the provided string as an integer.
    /// Returns a successful result containing the integer value if parsing is successful;
    /// otherwise, returns an error result indicating a failure in parsing.
    /// </summary>
    /// <param name="str">The string to parse, which can be null or empty.</param>
    /// <returns>A result containing either the parsed integer value or an error describing why parsing failed.</returns>
    public static Result<int> ParseInt(this string? str)
        => int.TryParse(str, out int value) ? value : (Error)"Invalid format";

    /// <summary>
    /// Attempts to parse the given string into a double-precision floating-point number.
    /// </summary>
    /// <param name="str">The string to parse into a double. May be null or empty.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> containing the successfully parsed double value,
    /// or an <see cref="Error"/> describing the parsing failure if the string is not a valid double.
    /// </returns>
    public static Result<double> ParseDouble(this string? str)
        => double.TryParse(str, out double value) ? value : (Error)"Invalid format";

    /// <summary>
    /// Attempts to parse the string into a boolean value.
    /// </summary>
    /// <param name="str">The string to parse as a boolean.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> containing the parsed boolean value if successful,
    /// or an <see cref="Error"/> if the string format is invalid.
    /// </returns>
    public static Result<bool> ParseBool(this string? str)
        => bool.TryParse(str, out bool value) ? value : (Error)"Invalid format";

    /// <summary>
    /// Parses the input string into a TimeSpan instance using the provided parser.
    /// </summary>
    /// <param name="str">The input string to parse as a TimeSpan. Can be null.</param>
    /// <param name="parser">An implementation of ITimeSpanParser used to perform the parsing.</param>
    /// <returns>
    /// A Result object containing the parsed TimeSpan if the operation is successful. If parsing fails, the Result will contain an error.
    /// </returns>
    public static Result<TimeSpan> ParseTimeSpan(this string? str, ITimeSpanParser parser)
        => parser.Parse(str);

    /// <summary>
    /// Parses a string representation of a time span into a Result containing a TimeSpan object or an error.
    /// </summary>
    /// <param name="str">The string to parse as a TimeSpan. Can be null.</param>
    /// <returns>
    /// A Result object containing the parsed TimeSpan value if the parsing is successful,
    /// otherwise an error indicating the failure.
    /// </returns>
    public static Result<TimeSpan> ParseTimeSpan(this string? str)
        => ITimeSpanParser.DefaultImpl.Parse(str);

    /// <summary>
    /// Parses a string representation of a TimeSpan using a provided format provider.
    /// </summary>
    /// <param name="str">The string representation of the TimeSpan to parse. Can be null.</param>
    /// <param name="formatProvider">The format provider used to parse the string into a TimeSpan.</param>
    /// <returns>A Result containing the parsed TimeSpan if successful, or an Error if the string format is invalid.</returns>
    public static Result<TimeSpan> ParseTimeSpan(this string? str, IFormatProvider formatProvider)
        => TimeSpan.TryParse(str, formatProvider, out TimeSpan value) ? value : (Error)"Invalid format";

    /// <summary>
    /// Parses the given string into a DateTime object.
    /// If the string is successfully parsed, it returns the resulting DateTime.
    /// Otherwise, it returns an error indicating invalid format.
    /// </summary>
    /// <param name="str">The string input to be parsed into a DateTime.</param>
    /// <returns>A Result object containing the parsed DateTime if successful,
    /// otherwise an error in case of failure.</returns>
    public static Result<DateTime> ParseDateTime(this string? str)
        => DateTime.TryParse(str, out DateTime value) ? value : (Error)"Invalid format";

    /// <summary>
    /// Parses the provided string into a <see cref="DateTime"/> instance using the specified format provider.
    /// </summary>
    /// <param name="str">The string to be parsed into a DateTime.</param>
    /// <param name="formatProvider">The format provider to interpret the string representation of DateTime.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> of type <see cref="DateTime"/> containing the parsed DateTime if successful,
    /// or an <see cref="Error"/> if the format is invalid.
    /// </returns>
    public static Result<DateTime> ParseDateTime(this string? str, IFormatProvider formatProvider)
        => DateTime.TryParse(str, formatProvider, out DateTime value) ? value : (Error)"Invalid format";
}