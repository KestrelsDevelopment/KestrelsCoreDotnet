using System.Diagnostics;
using Microsoft.Extensions.Hosting;

namespace KestrelsDev.KestrelsCore.Web.Diagnostics;

public interface IDiagnosticsService
{
    public DiagnosticsEvent StartTiming(string eventType);
}