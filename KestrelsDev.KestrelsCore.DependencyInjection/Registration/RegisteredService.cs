namespace KestrelsDev.KestrelsCore.DependencyInjection.Registration;

/// <summary>
/// Represents a definition for an injectable service.
/// </summary>
public readonly struct RegisteredService
{
    /// <summary>
    /// The <see cref="Type"/> of the injected service.
    /// </summary>
    public required Type ServiceType { get; init; }
    /// <summary>
    /// The behaviour when injecting multiple instances of this service.
    /// </summary>
    public required InjectionType InjectionType { get; init; }
    /// <summary>
    /// The function that is invoked to create an instance of this service.
    /// </summary>
    public required Func<IServiceScope, object> Factory { get; init; }
}
