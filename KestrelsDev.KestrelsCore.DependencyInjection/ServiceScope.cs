using System.Reflection;
using KestrelsDev.KestrelsCore.DependencyInjection.Exceptions;
using KestrelsDev.KestrelsCore.DependencyInjection.Registration;
using KestrelsDev.KestrelsCore.ResultPattern;

namespace KestrelsDev.KestrelsCore.DependencyInjection;

public class ServiceScope(IServiceRegistration registration) : IServiceScope
{
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