using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace KestrelsDev.KestrelsCore.Web.Extensions;

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