using System.Collections.ObjectModel;

namespace KestrelsDev.KestrelsCore.DependencyInjection.Registration;

/// <summary>
/// Provides an implementation of the <see cref="IServiceRegistration"/> interface
/// for registering service types with their concrete implementations or instances.
/// Enables dependency injection by acting as a registration mechanism for
/// services and their corresponding factories or singleton instances.
/// </summary>
public class ServiceRegistration : IServiceRegistration
{
    public ReadOnlyDictionary<Type, object> Registrations => new(RegistrationsInternal);

    private readonly Dictionary<Type, object> RegistrationsInternal = [];

    public IServiceRegistration Add<TService, TImpl>() where TImpl : TService, new()
        => AddInternal<TService>(typeof(TImpl));

    public IServiceRegistration Add<TService>() where TService : new()
        => AddInternal<TService>(typeof(TService));

    public IServiceRegistration Add<TService, TImpl>(TImpl singleton) where TImpl : TService
        => AddInternal<TService>(singleton);

    public IServiceRegistration Add<TService>(TService singleton)
        => AddInternal<TService>(singleton);

    public IServiceRegistration Add<TService, TImpl>(Func<TImpl> factory) where TImpl : TService
        => AddInternal<TService>(factory);

    public IServiceRegistration Add<TService>(Func<TService> factory)
        => AddInternal<TService>(factory);

    /// <summary>
    /// Adds a service registration for the specified service type <typeparamref name="TService"/>
    /// with the provided implementation or factory method.
    /// Ensures that the service is registered with the corresponding implementation details
    /// in the internal registration dictionary.
    /// </summary>
    /// <typeparam name="TService">The type of the service being registered.</typeparam>
    /// <param name="value">
    /// An object representing either the implementation type, a singleton instance,
    /// or a factory delegate for creating instances of the service.
    /// </param>
    /// <returns>
    /// The current instance of <see cref="ServiceRegistration"/>, enabling fluent API style chaining.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the provided <paramref name="value"/> is null.
    /// </exception>
    private ServiceRegistration AddInternal<TService>(object? value)
    {
        RegistrationsInternal[typeof(TService)] = value ?? throw new ArgumentNullException(nameof(value));

        return this;
    }
}