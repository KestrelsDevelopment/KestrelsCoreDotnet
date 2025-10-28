using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using KestrelsDev.KestrelsCore.Parsers;
using KestrelsDev.KestrelsCore.ResultPattern;

namespace KestrelsDev.KestrelsCore.Extensions;

public static class StringExtensions
{
    public static bool EqualsIgnoreCase(this string str, string other)
        => str.Equals(other, StringComparison.OrdinalIgnoreCase);

    public static bool IsNullOrEmpty([NotNullWhen(false)] this string? str) => string.IsNullOrEmpty(str);

    public static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? str) => string.IsNullOrWhiteSpace(str);

    public static bool IsEmpty(this string str) => str.Length == 0;

    public static bool IsEmptyOrWhiteSpace(this string str) => str.Length == 0 || str.Trim().Length == 0;

    public static bool IsMatch(
        this string str,
        [StringSyntax("Regex", "options")] string pattern,
        RegexOptions options = RegexOptions.Compiled,
        TimeSpan? timeout = null)
        => Regex.IsMatch(str, pattern, options, timeout ?? Regex.InfiniteMatchTimeout);

    public static MatchCollection Matches(
        this string str,
        [StringSyntax("Regex", "options")] string pattern,
        RegexOptions options = RegexOptions.Compiled,
        TimeSpan? timeout = null)
        => Regex.Matches(str, pattern, options, timeout ?? Regex.InfiniteMatchTimeout);

    public static Match Match(
        this string str,
        [StringSyntax("Regex", "options")] string pattern,
        RegexOptions options = RegexOptions.Compiled,
        TimeSpan? timeout = null)
        => Regex.Match(str, pattern, options, timeout ?? Regex.InfiniteMatchTimeout);

    public static string ReplaceMatches(
        this string str,
        [StringSyntax("Regex", "options")] string pattern,
        MatchEvaluator evaluator,
        RegexOptions options = RegexOptions.Compiled,
        TimeSpan? timeout = null)
        => Regex.Replace(str, pattern, evaluator, options, timeout ?? Regex.InfiniteMatchTimeout);

    public static Result<int> ParseInt(this string? str)
        => int.TryParse(str, out int value) ? value : (Error)"Invalid format";

    public static Result<double> ParseDouble(this string? str)
        => double.TryParse(str, out double value) ? value : (Error)"Invalid format";

    public static Result<bool> ParseBool(this string? str)
        => bool.TryParse(str, out bool value) ? value : (Error)"Invalid format";

    public static Result<TimeSpan> ParseTimeSpan(this string? str, ITimeSpanParser parser)
        => parser.Parse(str);

    public static Result<TimeSpan> ParseTimeSpan(this string? str)
        => ITimeSpanParser.DefaultImpl.Parse(str);

    public static Result<TimeSpan> ParseTimeSpan(this string? str, IFormatProvider formatProvider)
        => TimeSpan.TryParse(str, formatProvider, out TimeSpan value) ? value : (Error)"Invalid format";

    public static Result<DateTime> ParseDateTime(this string? str)
        => DateTime.TryParse(str, out DateTime value) ? value : (Error)"Invalid format";

    public static Result<DateTime> ParseDateTime(this string? str, IFormatProvider formatProvider)
        => DateTime.TryParse(str, formatProvider, out DateTime value) ? value : (Error)"Invalid format";
}