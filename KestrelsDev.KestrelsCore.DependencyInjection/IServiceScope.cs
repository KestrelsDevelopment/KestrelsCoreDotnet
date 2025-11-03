using KestrelsDev.KestrelsCore.DependencyInjection.Exceptions;
using KestrelsDev.KestrelsCore.ResultPattern;

namespace KestrelsDev.KestrelsCore.DependencyInjection;

/// <summary>
/// Represents a scope for managing service lifetimes, including transient and singleton instances,
/// and provides methods to create, resolve, and validate service configurations within the scope.
/// </summary>
public interface IServiceScope
{
    /// <summary>
    /// Creates and returns a new instance of the specified service type.
    /// </summary>
    /// <typeparam name="TService">The type of service to be instantiated.</typeparam>
    /// <returns>A new instance of the requested service type.</returns>
    /// <exception cref="NullInjectionException">
    /// Thrown if the requested service is not registered, the implementation lacks a valid public constructor,
    /// or the created instance is not of the requested type.
    /// </exception>
    public TService New<TService>();

    /// <summary>
    /// Resolves and returns a singleton instance of the specified service type within the current scope.
    /// If an instance of the specified type already exists in the current scope, the existing instance is returned.
    /// Otherwise, a new instance is created, stored, and returned.
    /// </summary>
    /// <typeparam name="TService">The type of the service to resolve as a singleton.</typeparam>
    /// <returns>An instance of the specified service type, shared within the current scope.</returns>
    /// <exception cref="NullInjectionException">
    /// Thrown when the creation of the service instance fails.
    /// </exception>
    public TService Singleton<TService>();

    /// <summary>
    /// Validates the registered service dependencies within the scope.
    /// </summary>
    /// <returns>
    /// A <see cref="Result"/> instance representing the outcome of the validation.
    /// Returns a successful result if all dependencies are correctly registered and valid;
    /// otherwise, returns a result containing one or more validation errors.
    /// </returns>
    public Result Validate();
}