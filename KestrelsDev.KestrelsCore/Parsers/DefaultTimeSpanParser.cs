using System.Text.RegularExpressions;
using KestrelsDev.KestrelsCore.Extensions;
using KestrelsDev.KestrelsCore.ResultPattern;

namespace KestrelsDev.KestrelsCore.Parsers;

public class DefaultTimeSpanParser : ITimeSpanParser
{
    public bool TryParse(string? s, out TimeSpan value)
    {
        value = TimeSpan.Zero;
        Result<TimeSpan> result = Parse(s);

        if (result.IsError)
            return false;

        value = result.Value;

        return true;
    }

    public Result<TimeSpan> Parse(string? s)
    {
        if (s.IsNullOrWhiteSpace())
            return (Error)"String is empty";

        if (TimeSpan.TryParse(s, out TimeSpan value))
            return value;

        s = s.Trim().Replace(" ", string.Empty);

        MatchCollection matches = s.Matches(@"(\d*)([^\d\s]*)");

        value = TimeSpan.Zero;

        try
        {
            foreach (Match match in matches)
            {
                value += ParseMatch(match);
            }

            return value;
        }
        catch (Exception e)
        {
            return new Error(e.Message, e);
        }
    }

    private static TimeSpan ParseMatch(Match match)
    {
        if (match.Groups[0].Length == 0)
            return TimeSpan.Zero;

        if (match.Groups[1].Length == 0 || match.Groups[2].Length == 0)
            throw new ArgumentException("Both value and unit must be given");

        double v = match.Groups[1].Value.ParseDouble();
        string u = match.Groups[2].Value.ToLower();

        return u switch
        {
            "d" => TimeSpan.FromDays(v),
            "h" => TimeSpan.FromHours(v),
            "m" => TimeSpan.FromMinutes(v),
            "s" => TimeSpan.FromSeconds(v),
            "ms" => TimeSpan.FromMilliseconds(v),
            _ => throw new ArgumentException("Invalid unit")
        };
    }
}