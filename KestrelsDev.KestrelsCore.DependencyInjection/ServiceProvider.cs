using KestrelsDev.KestrelsCore.DependencyInjection.Registration;

namespace KestrelsDev.KestrelsCore.DependencyInjection;

/// <summary>
/// Holds static references to a global <see cref="IServiceRegistration"/> and <see cref="IServiceScope"/>.
/// </summary>
public static class ServiceProvider
{
    /// <summary>
    /// The default global <see cref="IServiceRegistration"/>.
    /// </summary>
    public static readonly IServiceRegistration Registration = new ServiceRegistration();

    /// <summary>
    /// The default global <see cref="IServiceScope"/>.
    /// </summary>
    public static readonly IServiceScope DefaultScope = new ServiceScope(Registration);

    /// <summary>
    /// Creates a new child scope based on <see cref="DefaultScope"/>.
    /// </summary>
    private static IServiceScope CreateScope() => DefaultScope.CreateChildScope();
}