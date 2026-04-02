namespace KestrelsDev.KestrelsCore.DependencyInjection.Registration;

/// <summary>
/// Defines the behaviour that happens when injecting the same service multiple times.
/// </summary>
public enum InjectionType
{
    /// <summary>
    /// Represents a service that is recreated each time it is injected.
    /// </summary>
    Transient = 0,
    /// <summary>
    /// Represents a service that is reused within the same scope.
    /// </summary>
    Scoped = 1,
    /// <summary>
    /// Represents a service that is reused within the same scope tree.
    /// </summary>
    Singleton = 2
}
