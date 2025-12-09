namespace KestrelsDev.KestrelsCore.Web.Diagnostics;

public class DiagnosticsScope
{
    public int Id { get; } = Random.Shared.Next();
}