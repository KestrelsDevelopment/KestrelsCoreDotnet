namespace KestrelsDev.KestrelsCore.Web.Diagnostics;

public class DiagnosticsService(IDiagnosticsCollectorService collector, DiagnosticsScope scope) : IDiagnosticsService
{
    public DiagnosticsEvent StartTiming(string eventType) => collector.StartTiming(eventType, scope);
}