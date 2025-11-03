using System.Collections.ObjectModel;

namespace KestrelsDev.KestrelsCore.DependencyInjection.Registration;

/// <summary>
/// Represents a mechanism for registering service types with their concrete implementations,
/// allowing resolution of dependencies within a dependency injection container.
/// This interface supports various methods of service registration including type mapping,
/// singleton instances, and factory delegates.
/// </summary>
public interface IServiceRegistration
{
    /// <summary>
    /// Provides a read-only view of the service registrations, mapping service types
    /// to their respective implementations, factories, or singleton instances.
    /// </summary>
    /// <remarks>
    /// The <c>Registrations</c> property allows inspection of the service registration mappings
    /// within the dependency injection container. This ensures that access to the internal
    /// registration data remains consistent and immutable. It is crucial for managing and resolving
    /// services, enabling proper dependency injection functionality.
    /// </remarks>
    public ReadOnlyDictionary<Type, object> Registrations { get; }

    /// Adds a service registration where the service type is associated with a concrete implementation type
    /// using a parameterless constructor. The concrete type is resolved whenever the associated service type is requested.
    /// <typeparam name="TService">The service interface or type to be registered.</typeparam>
    /// <typeparam name="TImpl">The concrete implementation type of the service.</typeparam>
    /// <returns>An instance of IServiceRegistration to allow for chaining additional service registrations or configurations.</returns>
    public IServiceRegistration Add<TService, TImpl>() where TImpl : TService, new();

    /// Adds a service to the registration using a default constructor.
    /// The concrete implementation is registered to be resolved whenever the service type is requested.
    /// <typeparam name="TService">The type of the service to register.</typeparam>
    /// <returns>An instance of IServiceRegistration for method chaining or further registrations.</returns>
    public IServiceRegistration Add<TService>() where TService : new();

    /// Adds a service registration, associating the specified interface with a provided singleton implementation instance.
    /// The singleton instance is registered for all future resolutions of the service interface type.
    /// <typeparam name="TService">The type of the service interface to be registered.</typeparam>
    /// <typeparam name="TImpl">The type of the implementation class that implements the service interface.</typeparam>
    /// <param name="singleton">The instance of the implementation type to be registered as a singleton for the service interface.</param>
    /// <returns>The current instance of IServiceRegistration for method chaining or further registrations.</returns>
    public IServiceRegistration Add<TService, TImpl>(TImpl singleton) where TImpl : TService;

    /// Registers a service of the specified type as a singleton instance.
    /// The provided instance will be used whenever the service type is requested.
    /// <typeparam name="TService">The type of the service being registered.</typeparam>
    /// <param name="singleton">The instance of the service to register as a singleton.</param>
    /// <returns>An instance of IServiceRegistration for method chaining or further registrations.</returns>
    public IServiceRegistration Add<TService>(TService singleton);

    /// Adds a service and its implementation to the service registration.
    /// The implementation instance is created by invoking a user-provided factory method.
    /// <typeparam name="TService">The type of the service to be registered.</typeparam>
    /// <typeparam name="TImpl">The concrete implementation type of the specified service.</typeparam>
    /// <param name="factory">A factory function responsible for creating an instance of the implementation.</param>
    /// <returns>An instance of IServiceRegistration to allow for chaining further service registrations.</returns>
    public IServiceRegistration Add<TService, TImpl>(Func<TImpl> factory) where TImpl : TService;

    /// Adds a service registration where an instance of the service is created using a provided factory method.
    /// <typeparam name="TService">The type of the service to be registered.</typeparam>
    /// <param name="factory">A factory method that provides an instance of the service.</param>
    /// <returns>An updated instance of IServiceRegistration to allow for chaining additional registrations or further configuration.</returns>
    public IServiceRegistration Add<TService>(Func<TService> factory);
}