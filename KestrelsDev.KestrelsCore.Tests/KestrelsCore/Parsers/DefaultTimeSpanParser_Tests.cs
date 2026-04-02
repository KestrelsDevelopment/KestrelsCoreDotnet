using KestrelsDev.KestrelsCore.Parsers;
using KestrelsDev.KestrelsCore.ResultPattern;

namespace KestrelsDev.KestrelsCore.Tests.KestrelsCore.Parsers;

internal class DefaultTimeSpanParser_Tests
{
    [Test]
    [Arguments("1.00:00:00", 1, 0, 0, 0, 0)]
    [Arguments("01:00:00", 0, 1, 0, 0, 0)]
    [Arguments("00:01:00", 0, 0, 1, 0, 0)]
    [Arguments("00:00:01", 0, 0, 0, 1, 0)]
    [Arguments("00:00:00.0010000", 0, 0, 0, 0, 1)]
    [Arguments("1d", 1, 0, 0, 0, 0)]
    [Arguments("1h", 0, 1, 0, 0, 0)]
    [Arguments("1m", 0, 0, 1, 0, 0)]
    [Arguments("1s", 0, 0, 0, 1, 0)]
    [Arguments("1ms", 0, 0, 0, 0, 1)]
    [Arguments("", 0, 0, 0, 0, 0)]
    [Arguments("10d", 10, 0, 0, 0, 0)]
    [Arguments("5d4h", 5, 4, 0, 0, 0)]
    [Arguments("5d 4h", 5, 4, 0, 0, 0)]
    [Arguments("5d 4h 3m 2s 1ms", 5, 4, 3, 2, 1)]
    public async Task Parse__ValidFormat__ReturnsParsedTimeSpan(string inputString, int resultDays, int resultHours, int resultMinutes, int resultSeconds, int resultMillis)
    {
        DefaultTimeSpanParser parser = new();

        Result<TimeSpan> result = parser.Parse(inputString);

        await Assert.That(result.IsError).IsFalse();
        await Assert.That(result.Value.Days).EqualTo(resultDays);
        await Assert.That(result.Value.Hours).EqualTo(resultHours);
        await Assert.That(result.Value.Minutes).EqualTo(resultMinutes);
        await Assert.That(result.Value.Seconds).EqualTo(resultSeconds);
        await Assert.That(result.Value.Milliseconds).EqualTo(resultMillis);
    }

    [Test]
    [Arguments("invalid string")]
    [Arguments("1d1")]
    [Arguments("d")]
    [Arguments("d1")]
    [Arguments("1y")]
    public async Task Parse__InvalidFormat__ReturnsError(string inputString)
    {
        DefaultTimeSpanParser parser = new();

        Result<TimeSpan> result = parser.Parse(inputString);

        await Assert.That(result.IsError).IsTrue();
    }

    [Test]
    [Arguments("1.00:00:00", 1, 0, 0, 0, 0)]
    [Arguments("01:00:00", 0, 1, 0, 0, 0)]
    [Arguments("00:01:00", 0, 0, 1, 0, 0)]
    [Arguments("00:00:01", 0, 0, 0, 1, 0)]
    [Arguments("00:00:00.0010000", 0, 0, 0, 0, 1)]
    [Arguments("1d", 1, 0, 0, 0, 0)]
    [Arguments("1h", 0, 1, 0, 0, 0)]
    [Arguments("1m", 0, 0, 1, 0, 0)]
    [Arguments("1s", 0, 0, 0, 1, 0)]
    [Arguments("1ms", 0, 0, 0, 0, 1)]
    [Arguments("", 0, 0, 0, 0, 0)]
    [Arguments("10d", 10, 0, 0, 0, 0)]
    [Arguments("5d4h", 5, 4, 0, 0, 0)]
    [Arguments("5d 4h", 5, 4, 0, 0, 0)]
    [Arguments("5d 4h 3m 2s 1ms", 5, 4, 3, 2, 1)]
    public async Task TryParse__ValidFormat__ReturnsParsedTimeSpanAndTrue(string inputString, int resultDays, int resultHours, int resultMinutes, int resultSeconds, int resultMillis)
    {
        DefaultTimeSpanParser parser = new();

        bool success = parser.TryParse(inputString, out var result);

        await Assert.That(success).IsTrue();
        await Assert.That(result.Days).EqualTo(resultDays);
        await Assert.That(result.Hours).EqualTo(resultHours);
        await Assert.That(result.Minutes).EqualTo(resultMinutes);
        await Assert.That(result.Seconds).EqualTo(resultSeconds);
        await Assert.That(result.Milliseconds).EqualTo(resultMillis);
    }

    [Test]
    [Arguments("invalid string")]
    [Arguments("1d1")]
    [Arguments("d")]
    [Arguments("d1")]
    [Arguments("1y")]
    public async Task TryParse__InvalidFormat__ReturnsFalse(string inputString)
    {
        DefaultTimeSpanParser parser = new();

        bool success = parser.TryParse(inputString, out _);

        await Assert.That(success).IsFalse();
    }
}
