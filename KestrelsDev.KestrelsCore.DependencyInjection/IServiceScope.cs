using KestrelsDev.KestrelsCore.ResultPattern;
using KestrelsDev.KestrelsCore.DependencyInjection.Registration;
using KestrelsDev.KestrelsCore.DependencyInjection.Exceptions;

namespace KestrelsDev.KestrelsCore.DependencyInjection;

/// <summary>
/// Represents a container that automates the creation of instances based on service registrations and holds reusable instances.
/// </summary>
public interface IServiceScope
{
    /// <summary>
    /// Creates or reuses an instance of type <typeparamref name="TService"/>.
    /// </summary>
    /// <typeparam name="TService">The type of the service to retrieve.</typeparam>
    /// <returns>The retrieved service.</returns>
    /// <remarks>Throws an exception if the requested service is not registered or unable to be constructed.</remarks>
    /// <exception cref="NullInjectionException"></exception>
    public TService Get<TService>();

    /// <summary>
    /// Creates or reuses an instance of type <typeparamref name="TService"/>.
    /// </summary>
    /// <typeparam name="TService">The type of the service to retrieve.</typeparam>
    /// <returns>A <see cref="Result{T}"/> object wrapping either the requested service, or an error if retrieval failed.</returns>
    public Result<TService> TryGet<TService>();

    /// <summary>
    /// Creates or reuses a keyed instance of type <typeparamref name="TService"/>.
    /// </summary>
    /// <typeparam name="TService">The type of the service to retrieve.</typeparam>
    /// <param name="key">The key that the service was registered under.</param>
    /// <returns>The retrieved service.</returns>
    /// <remarks>Throws an exception if the requested service is not registered or unable to be constructed.</remarks>
    /// <exception cref="NullInjectionException"></exception>
    public TService GetKeyed<TService>(object key);

    /// <summary>
    /// Creates or reuses a keyed instance of type <typeparamref name="TService"/>.
    /// </summary>
    /// <typeparam name="TService">The type of the service to retrieve.</typeparam>
    /// <param name="key">The key that the service was registered under.</param>
    /// <returns>A <see cref="Result{T}"/> object wrapping either the requested service, or an error if retrieval failed.</returns>
    public Result<TService> TryGetKeyed<TService>(object key);

    /// <summary>
    /// Creates or reuses an instance of type <paramref name="serviceType"/>.
    /// </summary>
    /// <param name="serviceType">The type of the service to retrieve.</param>
    /// <returns>The retrieved service.</returns>
    /// <remarks>Throws an exception if the requested service is not registered or unable to be constructed.</remarks>
    /// <exception cref="NullInjectionException"></exception>
    public object Get(Type serviceType);

    /// <summary>
    /// Creates or reuses an instance of type <paramref name="serviceType"/>.
    /// </summary>
    /// <param name="serviceType">The type of the service to retrieve.</param>
    /// <returns>A <see cref="Result{T}"/> object wrapping either the requested service, or an error if retrieval failed.</returns>
    public Result<object> TryGet(Type serviceType);

    /// <summary>
    /// Creates or reuses a keyed instance of type <paramref name="serviceType"/>.
    /// </summary>
    /// <param name="serviceType">The type of the service to retrieve.</param>
    /// <param name="key">The key that the service was registered under.</param>
    /// <returns>The retrieved service.</returns>
    /// <remarks>Throws an exception if the requested service is not registered or unable to be constructed.</remarks>
    /// <exception cref="NullInjectionException"></exception>
    public object GetKeyed(Type serviceType, object key);

    /// <summary>
    /// Creates or reuses a keyed instance of type <paramref name="serviceType"/>.
    /// </summary>
    /// <param name="serviceType">The type of the service to retrieve.</param>
    /// <param name="key">The key that the service was registered under.</param>
    /// <returns>A <see cref="Result{T}"/> object wrapping either the requested service, or an error if retrieval failed.</returns>
    public Result<object> TryGetKeyed(Type serviceType, object key);

    /// <summary>
    /// Checks whether all registered services are able to be constructed.
    /// </summary>
    /// <returns>A result indicating the success or failure of the validation. 
    /// If validation failed it will hold a <see cref="AggregateError"/> object containing all errors that occurred.</returns>
    public Result Validate();

    /// <summary>
    /// Creates a new <see cref="IServiceScope"/> based on this scope. It uses the same <see cref="IServiceRegistration"/> and 
    /// shares the same instances for services with injection type <see cref="InjectionType.Singleton"/>.
    /// </summary>
    /// <returns>A new <see cref="IServiceScope"/> based on this scope.</returns>
    public IServiceScope CreateChildScope();
}