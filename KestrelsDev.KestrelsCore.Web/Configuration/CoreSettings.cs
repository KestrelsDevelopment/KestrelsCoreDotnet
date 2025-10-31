using KestrelsDev.KestrelsCore.Extensions;
using Serilog;

namespace KestrelsDev.KestrelsCore.Configuration;

public class CoreSettings()
{
    private static readonly TimeSpan HealthCheckIntervalMinValue = TimeSpan.FromSeconds(5);
    private static readonly TimeSpan HealthCheckIntervalDefaultValue = TimeSpan.FromSeconds(60);

    internal static CoreSettings Init(CoreSettingsModel model)
    {
        TimeSpan healthCheckInterval = model.HealthCheckInterval.IsNullOrWhiteSpace()
            ? HealthCheckIntervalDefaultValue
            : model.HealthCheckInterval.ParseTimeSpan().Value;

        if (healthCheckInterval < HealthCheckIntervalMinValue)
        {
            Log.Warning(
                "Value for {Field} out of range. Its value must be at least {MinValue}. Reverting to default value {Default}.",
                $"{nameof(CoreSettings)}.{nameof(CoreSettingsModel.HealthCheckInterval)}", HealthCheckIntervalMinValue,
                HealthCheckIntervalDefaultValue);

            healthCheckInterval = HealthCheckIntervalDefaultValue;
        }

        return new()
        {
            HealthCheckInterval = healthCheckInterval
        };
    }

    public TimeSpan HealthCheckInterval { get; init; }

    internal class CoreSettingsModel
    {
        public string? HealthCheckInterval { get; set; }

        public static implicit operator CoreSettings(CoreSettingsModel model) => Init(model);
    }
}