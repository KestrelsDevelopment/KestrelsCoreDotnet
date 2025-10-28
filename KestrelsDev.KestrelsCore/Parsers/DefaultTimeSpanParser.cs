using KestrelsDev.KestrelsCore.Extensions;

namespace KestrelsDev.KestrelsCore.Parsers;

public class DefaultTimeSpanParser : ITimeSpanParser
{

    public bool TryParse(string? s, out TimeSpan value)
    {
        value = TimeSpan.Zero;

        if (s.IsNullOrWhiteSpace())
            return false;

        if (TimeSpan.TryParse(s, out value))
            return true;

        return false;
    }
}