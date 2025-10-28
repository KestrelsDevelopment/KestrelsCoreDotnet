using KestrelsDev.KestrelsCore.ResultPattern;

namespace KestrelsDev.KestrelsCore.Parsers;

public interface ITimeSpanParser
{
    public static readonly ITimeSpanParser DefaultImpl = new DefaultTimeSpanParser();

    public bool TryParse(string? s, out TimeSpan value);

    public Result<TimeSpan> Parse(string? s);
}