using System.Reflection;
using KestrelsDev.KestrelsCore.DependencyInjection.Exceptions;
using KestrelsDev.KestrelsCore.DependencyInjection.Registration;
using KestrelsDev.KestrelsCore.ResultPattern;

namespace KestrelsDev.KestrelsCore.DependencyInjection;

/// <summary>
/// Represents a scope for managing dependency injection services.
/// </summary>
/// <remarks>
/// This class provides methods to resolve and manage dependencies within the current service scope.
/// It supports creating instances, managing singletons, and validating service registrations.
/// </remarks>
public class ServiceScope(IServiceRegistration registration) : IServiceScope
{
    /// <summary>
    /// Stores instances of singleton services within the scope of the dependency injection container.
    /// </summary>
    /// <remarks>
    /// The Singletons dictionary stores already-created instances of services to ensure they are only
    /// instantiated once during the service's lifetime within the scope. Each service type is used as
    /// the key, and its corresponding instance is the value.
    /// </remarks>
    private readonly Dictionary<Type, object> Singletons = [];

    public TService New<TService>()
    {
        Type typeService = typeof(TService);

        if (!registration.Registrations.TryGetValue(typeService, out object? registered))
            throw new NullInjectionException($"Service is not registered in this scope");

        switch (registered)
        {
            case TService service:
                return service;
            case Func<TService> factory:
                return factory.Invoke();
        }

        if (registered is not Type typeImpl)
            throw new NullInjectionException(
                $"Invalid registration: object is neither {nameof(TService)}, {nameof(Func<TService>)} or {nameof(Type)}");

        ConstructorInfo? constructor = typeImpl.GetConstructor([]);

        if (constructor is null || !constructor.IsPublic)
            throw new NullInjectionException("Invalid registration: Implementation has not valid constructor");

        object? constructed = constructor.Invoke([]);

        return constructed is TService constructedService
            ? constructedService
            : throw new NullInjectionException($"Invalid registration: Constructed object is not {typeof(TService)}");
    }

    public TService Singleton<TService>()
    {
        Type type = typeof(TService);

        if (registration.Registrations.TryGetValue(type, out object? value) && value is TService singleton)
            return singleton;

        if (Singletons.TryGetValue(type, out value) && value is TService service)
            return service;

        service = New<TService>() ?? throw new NullInjectionException("Failed to create instance");
        Singletons[type] = service;

        return service;
    }

    public Result Validate()
    {
        List<Error> errors = [];

        foreach (Type type in registration.Registrations.Keys)
        {
            ValidateInternal(type).Catch((Error err) => errors.Add(err));
        }

        return errors.Count == 0 ? true : (Error)errors;
    }

    /// <summary>
    /// Validates the ability to create an instance of the specified type within the service scope.
    /// </summary>
    /// <param name="type">The type of the service to be validated.</param>
    /// <returns>
    /// A <see cref="Result"/> indicating the success or failure of the validation.
    /// Returns a success result if an instance of the type can be successfully created.
    /// If validation fails due to an error, the result contains the error information.
    /// </returns>
    private Result ValidateInternal(Type type)
    {
        MethodInfo? method = GetType().GetMethod(nameof(New));

        if (method is null)
            throw new NullReferenceException($"Method {nameof(New)} not found");

        method = method.MakeGenericMethod(type);

        try
        {
            method.Invoke(this, null);

            return true;
        }
        catch (Exception e)
        {
            return (Error)e;
        }
    }
}