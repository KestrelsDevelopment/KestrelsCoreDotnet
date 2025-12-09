using System.Collections.Concurrent;
using KestrelsDev.KestrelsCore.Web.EntityFramework;
using Microsoft.Extensions.Logging;

namespace KestrelsDev.KestrelsCore.Web.Diagnostics;

public class DiagnosticsCollectorService<TDbContext>(TDbContext dbContext, ILogger<DiagnosticsCollectorService<TDbContext>> logger)
    : IDiagnosticsCollectorService where TDbContext : KestrelsDbContext
{
    private readonly ConcurrentBag<DiagnosticsEvent> PendingEvents = [];

    public DiagnosticsEvent StartTiming(string eventType, DiagnosticsScope scope)
    {
        DiagnosticsEvent evt = new(eventType, scope.Id, Callback);

        return evt;

        void Callback(DiagnosticsEvent e) => PendingEvents.Add(e);
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(60), CancellationToken.None);
                List<DiagnosticsEvent> events = PendingEvents.ToList();

                try
                {
                    PendingEvents.Clear();
                    List<DiagnosticsEvent.Entity> entities = events.Select(e => (DiagnosticsEvent.Entity)e).ToList();

                    dbContext.DiagnosticsEvents.AddRange(entities);
                    await dbContext.SaveChangesAsync(cancellationToken);
                }
                catch (Exception e)
                {
                    logger.LogError(e, "Error saving diagnostics events");

                    foreach (DiagnosticsEvent evt in events)
                    {
                        PendingEvents.Add(evt);
                    }
                }
            }
        }
        catch (TaskCanceledException)
        {
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}