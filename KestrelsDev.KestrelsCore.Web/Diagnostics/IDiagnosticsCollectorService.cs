using System.Diagnostics;
using Microsoft.Extensions.Hosting;

namespace KestrelsDev.KestrelsCore.Web.Diagnostics;

public interface IDiagnosticsCollectorService: IHostedService
{
    public DiagnosticsEvent StartTiming(string eventType, DiagnosticsScope scope);
}