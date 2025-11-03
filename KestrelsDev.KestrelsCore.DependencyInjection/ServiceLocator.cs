using KestrelsDev.KestrelsCore.DependencyInjection.Exceptions;
using KestrelsDev.KestrelsCore.DependencyInjection.Registration;

namespace KestrelsDev.KestrelsCore.DependencyInjection;

/// <summary>
/// Provides a static service locator for managing dependency injection within an application.
/// This class centralizes service registration and resolution, allowing access to new or
/// singleton instances of services.
/// </summary>
/// <remarks>
/// ServiceLocator uses an underlying service registration mechanism and scope to
/// facilitate dependency management. It exposes methods for retrieving services
/// either as new instances or singletons. This class is not intended for instantiation.
/// </remarks>
public static class ServiceLocator
{
    /// <summary>
    /// Represents the global service registration object used for resolving dependencies
    /// within the application. It facilitates the registration of service types with their
    /// implementations and supports singleton, transient, and factory-based registrations.
    /// This object is static and shared across the application, serving as the entry point
    /// for managing dependency injection configurations.
    /// </summary>
    public static readonly IServiceRegistration Registration = new ServiceRegistration();

    /// <summary>
    /// Represents the default service scope for the application. Provides an instance of
    /// <see cref="IServiceScope"/> that manages the creation and resolution of services.
    /// This scope is initialized using the default service registration instance and can be
    /// utilized to resolve transient or singleton service lifetimes through methods such as
    /// <see cref="IServiceScope.New{TService}"/> or <see cref="IServiceScope.Singleton{TService}"/>.
    /// Use this scope to ensure consistency in service resolution across the application.
    /// </summary>
    public static readonly IServiceScope DefaultScope = CreateScope();

    /// <summary>
    /// Creates a new instance of the default service scope for managing dependency injection.
    /// </summary>
    /// <returns>
    /// A new <see cref="ServiceScope"/> instance initialized with the default service registration.
    /// </returns>
    private static ServiceScope CreateScope() => new(Registration);

    /// <summary>
    /// Creates and returns a new instance of the specified service type
    /// within the default service scope.
    /// </summary>
    /// <typeparam name="TService">The type of service to be instantiated.</typeparam>
    /// <returns>A new instance of the requested service type.</returns>
    /// <exception cref="NullInjectionException">
    /// Thrown if the requested service is not registered, the implementation lacks a valid public constructor,
    /// or the created instance is not of the requested type.
    /// </exception>
    public static TService New<TService>() => DefaultScope.New<TService>();

    /// <summary>
    /// Resolves and returns a singleton instance of the specified service type by utilizing the default service scope.
    /// If an instance of the specified type already exists in the default scope, the existing instance is returned.
    /// Otherwise, a new instance is created, registered as a singleton, and returned.
    /// </summary>
    /// <typeparam name="TService">The type of the service to resolve as a singleton.</typeparam>
    /// <returns>An instance of the specified service type, shared within the default scope.</returns>
    /// <exception cref="NullInjectionException">
    /// Thrown when the creation of the service instance fails.
    /// </exception>
    public static TService Singleton<TService>() => DefaultScope.Singleton<TService>();
}