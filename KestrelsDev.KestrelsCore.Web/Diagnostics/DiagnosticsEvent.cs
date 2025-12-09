using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace KestrelsDev.KestrelsCore.Web.Diagnostics;

public class DiagnosticsEvent
{
    public DiagnosticsEvent(string type, int scopeId, Action<DiagnosticsEvent> finishCallback)
    {
        EventType = type;
        ScopeId = scopeId;
        FinishCallback = finishCallback;
        StartTime = DateTime.UtcNow;
        Stopwatch = new();

        Stopwatch.Start();
    }

    public string EventType { get; }

    public int ScopeId { get; }

    public DateTime StartTime { get; }

    public DateTime EndTime => StartTime + Duration;

    public TimeSpan Duration { get; private set; }

    private readonly Action<DiagnosticsEvent> FinishCallback;
    private readonly Stopwatch Stopwatch;

    public DiagnosticsEvent Finish()
    {
        Duration = Stopwatch.Elapsed;
        FinishCallback.Invoke(this);

        return this;
    }

    public class Entity
    {
        public int Id { get; set; }

        public int ScopeId { get; set; }

        [MaxLength(100)]
        public string EventName { get; set; } = string.Empty;

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public static implicit operator Entity(DiagnosticsEvent evt) => new()
            { ScopeId = evt.ScopeId, EventName = evt.EventType, StartTime = evt.StartTime, EndTime = evt.EndTime };
    }
}