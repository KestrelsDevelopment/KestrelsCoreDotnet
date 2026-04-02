using KestrelsDev.KestrelsCore.DependencyInjection.Exceptions;
using KestrelsDev.KestrelsCore.DependencyInjection.Registration;
using KestrelsDev.KestrelsCore.ResultPattern;

namespace KestrelsDev.KestrelsCore.DependencyInjection;

public class ServiceScope(IServiceRegistration registration) : IServiceScope
{
    private IServiceScope? _parentScope;

    private ServiceScope(IServiceRegistration registration, IServiceScope parentScope) : this(registration)
    {
        _parentScope = parentScope;
    }

    public IServiceScope CreateChildScope()
        => new ServiceScope(registration, this);

    public TService Get<TService>()
        => (TService)Get(typeof(TService));

    public object Get(Type serviceType)
        => GetKeyed(serviceType, "");

    public TService GetKeyed<TService>(object key)
        => (TService)GetKeyed(typeof(TService), key);

    public object GetKeyed(Type serviceType, object key)
    {
        RegisteredService? service = registration.GetKeyedDefinition(serviceType, key);

        if (service is null)
            throw new NullInjectionException(serviceType, "No definition found");

        object constructed = service?.Factory(this)!;

        if (!constructed.GetType().IsAssignableTo(serviceType))
            throw new NullInjectionException(serviceType, "Constructed object is of unexpected type");

        return constructed;
    }

    public Result<TService> TryGet<TService>()
        => TryGet(typeof(TService)).Map(
            v => new Result<TService>((TService)v),
            e => new Result<TService>(e));

    public Result<object> TryGet(Type serviceType)
        => Result.From(() => Get(serviceType));

    public Result<TService> TryGetKeyed<TService>(object key)
        => TryGetKeyed(typeof(TService), key).Map(
            v => new Result<TService>((TService)v),
            e => new Result<TService>(e));

    public Result<object> TryGetKeyed(Type serviceType, object key)
        => Result.From(() => GetKeyed(serviceType, key));

    public Result Validate()
    {
        List<Error> errors = [];

        foreach (var serviceType in registration.Register.Values)
            foreach (var service in serviceType.Values)
            {
                try
                {
                    service.Factory(this);
                }
                catch (Exception ex)
                {
                    errors.Add(ex);
                }
            }

        if (errors.Count == 0)
            return true;

        return new AggregateError($"One or more service failed validation. See {nameof(AggregateError.Errors)} for details.", errors);
    }
}