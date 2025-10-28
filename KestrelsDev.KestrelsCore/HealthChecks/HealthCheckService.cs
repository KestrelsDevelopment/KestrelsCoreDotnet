using KestrelsDev.KestrelsCore.Configuration;

namespace KestrelsDev.KestrelsCore.HealthChecks;

public abstract class HealthCheckService(CoreSettings coreSettings) : IHostedService
{
    protected abstract Task<bool> PerformCheckInternalAsync(CancellationToken cancellationToken);

    protected virtual TimeSpan HealthCheckInterval => coreSettings.HealthCheckInterval;

    public bool HealthStatus { get; protected set; }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _ = PerformChecksOnInterval(cancellationToken);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    protected async Task PerformChecksOnInterval(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            await PerformCheckAsync(cancellationToken);
            await Task.Delay(HealthCheckInterval, cancellationToken);
        }
    }

    public async Task<bool> PerformCheckAsync(CancellationToken? cancellationToken = null)
    {
        HealthStatus = await PerformCheckInternalAsync(cancellationToken ?? CancellationToken.None);

        return HealthStatus;
    }
}