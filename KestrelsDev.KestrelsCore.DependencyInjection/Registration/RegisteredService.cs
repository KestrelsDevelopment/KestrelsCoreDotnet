namespace KestrelsDev.KestrelsCore.DependencyInjection.Registration;

public readonly struct RegisteredService
{
    public required Type ServiceType { get; init; }
    public required InjectionType InjectionType { get; init; }
    public required Func<IServiceScope, object> Factory { get; init; }
}
