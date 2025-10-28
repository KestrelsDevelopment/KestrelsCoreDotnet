using System.Diagnostics.CodeAnalysis;

namespace KestrelsDev.KestrelsCore.Extensions;

public static class LoggerExtensions
{
    [DoesNotReturn]
    [SuppressMessage("Usage", "CA2254:Template should be a static expression")]
    public static void LogFatal(this ILogger logger, string? message, params object?[] args)
    {
        logger.LogCritical(message, args);
        Environment.Exit(1);
    }
}